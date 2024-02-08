using DocumentsAPI.Models;
using System.Text.Json;

namespace DocumentsAPI.Services.Providers
{
    public class FileStorageProvider : IStorageProvider
    {
        private readonly string _storagePath;

        public FileStorageProvider(IConfiguration configuration)
        {
            _storagePath = configuration.GetValue<string>("FileStorage:Path"); ;
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<Document> GetByIdAsync(Guid id)
        {
            string filePath = GetFilePath(id);
            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var inputStream = new FileStream(filePath, FileMode.Open))
            {
                return await JsonSerializer.DeserializeAsync<Document>(inputStream);
            }
        }

        public async Task<Document> CreateAsync(Document document)
        {
            string filePath = GetFilePath(document.Id);
            using (var outputStream = new FileStream(filePath, FileMode.CreateNew))
            {
                await JsonSerializer.SerializeAsync(outputStream, document);
            }
            return document;
        }

        public async Task<Document> UpdateAsync(Guid id, Document updatedDocument)
        {
            string filePath = GetFilePath(id);
            using (var outputStream = new FileStream(filePath, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(outputStream, updatedDocument);
            }
            return updatedDocument;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            string filePath = GetFilePath(id);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        private string GetFilePath(Guid id) => Path.Combine(_storagePath, $"{id}.json");
    }
}
