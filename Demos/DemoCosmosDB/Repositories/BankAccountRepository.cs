using Microsoft.Azure.Cosmos;
using DemoCosmosDB.Models;

namespace DemoCosmosDB.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly Container _container;

        public BankAccountRepository(
            CosmosClient cosmosClient,
            IConfiguration config)
        {
            string database = config["CosmosDb:DatabaseName"]!;
            string container = config["CosmosDb:ContainerName"]!;

            _container = cosmosClient
                            .GetContainer(database, container);
        }

        public async Task<BankAccount> CreateAsync(
            BankAccount account)
        {
            ItemResponse<BankAccount> response =
                await _container.CreateItemAsync(
                    account,
                    new PartitionKey(account.CustomerId));

            return response.Resource;
        }

        public async Task<BankAccount> GetAsync(
            string accountId,
            string customerId)
        {
            try
            {
                ItemResponse<BankAccount> response =
                await _container.ReadItemAsync<BankAccount>(
                        accountId,
                        new PartitionKey(customerId));

                return response.Resource;
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode ==
                    System.Net.HttpStatusCode.NotFound)
                    return null!;

                throw;
            }
        }

        public async Task<IEnumerable<BankAccount>>
            GetAllByCustomerAsync(string customerId)
        {
            var query = new QueryDefinition(
                "SELECT * FROM c WHERE c.customerId = @customerId")
                .WithParameter("@customerId", customerId);

            List<BankAccount> accounts = new();

            FeedIterator<BankAccount> iterator =
                _container.GetItemQueryIterator<BankAccount>(query);

            while (iterator.HasMoreResults)
            {
                FeedResponse<BankAccount> response =
                    await iterator.ReadNextAsync();

                accounts.AddRange(response);
            }

            return accounts;
        }

        public async Task<BankAccount> UpdateAsync(
            BankAccount account)
        {
            ItemResponse<BankAccount> response =
                await _container.UpsertItemAsync(
                    account,
                    new PartitionKey(account.CustomerId));

            return response.Resource;
        }

        public async Task DeleteAsync(
            string accountId,
            string customerId)
        {
            await _container.DeleteItemAsync<BankAccount>(
                accountId,
                new PartitionKey(customerId));
        }
    }
}