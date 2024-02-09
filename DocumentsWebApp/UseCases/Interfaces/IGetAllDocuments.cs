using DocumentsAPI.Models;

namespace DocumentsAPI.UseCases.Interfaces
{
    public interface IGetAllDocuments
    {
        IAsyncEnumerable<Document> HandleAsync();
    }
}
