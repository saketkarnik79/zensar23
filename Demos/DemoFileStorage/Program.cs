using Microsoft.Extensions.Configuration;
using DemoFileStorage.Services;

namespace DemoFileStorage
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: false)
                .Build();
            string cs = configuration.GetConnectionString("cs")!;

            FileShareStorageService fileService = new FileShareStorageService(cs);

            System.Console.WriteLine("==========File Share==========");
            await fileService.UploadFileAsync("Program.cs");
            System.Console.WriteLine("Completed...");
        }
    }
}