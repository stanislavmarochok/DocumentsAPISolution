using DocumentsAPI.Models;

namespace DocumentsAPI.Services
{
    public interface IDocumentService
    {
        Task<Document> CreateAsync(Document document);
        Task<Document> UpdateAsync(Guid id, Document document);
        Task<Document> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
