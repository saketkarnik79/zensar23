using DemoBlobStorage.Services;
using Microsoft.Extensions.Configuration;

namespace DemoBlobStorage
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
            string cs = configuration.GetConnectionString("cs")!;

            var blobService = new BlobStorageService(cs);

            System.Console.WriteLine("=============BLOB=============");
            string name = "Sample.txt";
            await blobService.UploadBlobAsync(name);
            System.Console.WriteLine();
            await blobService.ListBlobsAsync();
            System.Console.WriteLine();
            await blobService.DownloadBlobAsync(name, name.Replace(".txt", "-Downloaded.txt"));

        }
    }
}
