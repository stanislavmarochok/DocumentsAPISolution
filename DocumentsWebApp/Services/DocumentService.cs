using DocumentsAPI.Models;
using DocumentsAPI.Services.Providers;

namespace DocumentsAPI.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IStorageProvider _storageProvider;

        public DocumentService(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public Task<Document> CreateAsync(Document document)
        {
            return _storageProvider.CreateAsync(document);
        }

        public Task<Document> GetByIdAsync(Guid id)
        {
            return _storageProvider.GetByIdAsync(id);
        }

        public Task<Document> UpdateAsync(Guid id, Document document)
        {
            return _storageProvider.UpdateAsync(id, document);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _storageProvider.DeleteAsync(id);
        }
    }
}
