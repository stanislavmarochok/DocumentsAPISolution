using DocumentsAPI.Models;

namespace DocumentsAPI.UseCases.Interfaces
{
    public interface IGetDocumentById
    {
        Task<Document> HandleAsync(Guid documentId);
    }
}
