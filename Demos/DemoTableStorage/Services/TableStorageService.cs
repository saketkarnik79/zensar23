using Azure;
using Azure.Data.Tables;
using DemoTableStorage.Models;

namespace DemoTableStorage.Services
{
    public class TableStorageService
    {
        private readonly string _connectionString;
        private const string TableName = "Employees";

        public TableStorageService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddEmployeeAsync()
        {
            TableClient tableClient =
                new TableClient(_connectionString, TableName);

            await tableClient.CreateIfNotExistsAsync();

            EmployeeEntity employee = new EmployeeEntity
            {
                PartitionKey = "IT",
                RowKey = Guid.NewGuid().ToString(),
                Name = "Saket Karnik",
                Department = "Delivery"
            };

            await tableClient.AddEntityAsync(employee);

            Console.WriteLine("Employee Added");
        }

        public async Task GetEmployeesAsync()
        {
            TableClient tableClient =
                new TableClient(_connectionString, TableName);

            Pageable<EmployeeEntity> employees =
                tableClient.Query<EmployeeEntity>();

            foreach (var emp in employees)
            {
                Console.WriteLine(
                    $"{emp.Name} - {emp.Department}");
            }
        }
    }
}
