using DocumentsAPI.Models;
using System.Collections.Concurrent;

namespace DocumentsAPI.Services.Providers
{
    public class InMemoryStorageProvider : IStorageProvider
    {
        private readonly ConcurrentDictionary<Guid, Document> _documents = new();

        public async Task<Document> CreateAsync(Document document)
        {
            _documents[document.Id] = document;
            return await Task.FromResult(document);
        }

        public async Task<Document> UpdateAsync(Guid id, Document document)
        {
            _documents[id] = document;
            return await Task.FromResult(document);
        }

        public async Task<Document> GetByIdAsync(Guid id)
        {
            _documents.TryGetValue(id, out var document);
            return await Task.FromResult(document);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = _documents.Remove(id, out _);
            return await Task.FromResult(result);
        }
    }
}
