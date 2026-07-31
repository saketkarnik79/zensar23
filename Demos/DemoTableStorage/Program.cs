using Microsoft.Extensions.Configuration;
using DemoTableStorage.Services;

namespace DemoTableStorage
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();

            string cs = configuration.GetConnectionString("cs")!;

            var tableService = new TableStorageService(cs);

            System.Console.WriteLine("==========Table==========");
            await tableService.AddEmployeeAsync();
            System.Console.WriteLine();
            await tableService.GetEmployeesAsync();
        }
    }
}