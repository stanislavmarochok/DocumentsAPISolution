using DocumentsAPI.Models;

namespace DocumentsAPI.UseCases.Interfaces
{
    public interface ICreateDocument
    {
        Task<Document> HandleAsync(Document newDocument);
    }
}
