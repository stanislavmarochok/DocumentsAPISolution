using DocumentsAPI.Models;

namespace DocumentsAPI.UseCases.Interfaces
{
    public interface IUpdateDocument
    {
        Task<Document> HandleAsync(Guid documentId, Document newDocument);
    }
}
