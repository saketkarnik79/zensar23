using DemoQueueStorage.Services;
using Microsoft.Extensions.Configuration;

namespace DemoQueueStorage
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
            string cs = configuration.GetConnectionString("cs")!;
            var queueService = new QueueStorageService(cs);

            System.Console.WriteLine("============QUEUE=============");
            //await queueService.SendMessageAsync("Order #1001 Created");
            await queueService.ReceiveMessageAsync();
        }
    }
}
