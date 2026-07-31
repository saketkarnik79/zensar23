using Azure.Storage.Files.Shares;

namespace DemoFileStorage.Services
{
    public class FileShareStorageService
    {
        private readonly string _connectionString;
        private const string ShareName = "projectfiles";

        public FileShareStorageService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UploadFileAsync(string localFilePath)
        {
            ShareClient shareClient =
                new ShareClient(_connectionString, ShareName);

            await shareClient.CreateIfNotExistsAsync();

            ShareDirectoryClient rootDirectory =
                shareClient.GetRootDirectoryClient();

            string fileName = Path.GetFileName(localFilePath);

            ShareFileClient fileClient =
                rootDirectory.GetFileClient(fileName);

            using FileStream stream =
                File.OpenRead(localFilePath);

            await fileClient.CreateAsync(stream.Length);

            await fileClient.UploadAsync(stream);

            Console.WriteLine($"Uploaded File: {fileName}");
        }

        public async Task DownloadFileAsync(
            string fileName,
            string targetPath)
        {
            ShareClient shareClient =
                new ShareClient(_connectionString, ShareName);

            ShareDirectoryClient rootDirectory =
                shareClient.GetRootDirectoryClient();

            ShareFileClient fileClient =
                rootDirectory.GetFileClient(fileName);

            using FileStream fileStream =
                File.OpenWrite(targetPath);

            var download = await fileClient.DownloadAsync();

            await download.Value.Content.CopyToAsync(fileStream);

            Console.WriteLine("File Downloaded");
        }
    }
}
