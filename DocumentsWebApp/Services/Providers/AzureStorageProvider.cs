using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DocumentsAPI.Models;
using System.Text.Json;

namespace DocumentsAPI.Services.Providers
{
    public class AzureStorageProvider : IStorageProvider
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName;

        public AzureStorageProvider(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("AzureStorage:ConnectionString");
            _blobContainerName = configuration.GetValue<string>("AzureStorage:ContainerName");
            _blobServiceClient = new BlobServiceClient(connectionString);
            _blobServiceClient.GetBlobContainerClient(_blobContainerName).CreateIfNotExists();
        }

        public async Task<Document> GetByIdAsync(Guid id)
        {
            var blobClient = GetBlobClient(id);
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                return await JsonSerializer.DeserializeAsync<Document>(response.Value.Content);
            }
            return null;
        }

        public async Task<Document> CreateAsync(Document document)
        {
            var blobClient = GetBlobClient(document.Id);
            using (var ms = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(ms, document);
                ms.Position = 0; // Reset the memory stream position to the beginning.
                await blobClient.UploadAsync(ms, new BlobHttpHeaders { ContentType = "application/json" });
            }
            return document;
        }

        public async Task<Document> UpdateAsync(Guid id, Document updatedDocument)
        {
            // Azure Blob Storage does not differentiate between create and update,
            // so this method can simply call CreateAsync.
            return await CreateAsync(updatedDocument);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var blobClient = GetBlobClient(id);
            var response = await blobClient.DeleteIfExistsAsync();
            return response.Value;
        }

        private BlobClient GetBlobClient(Guid documentId)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            return containerClient.GetBlobClient($"{documentId}.json");
        }
    }
}
