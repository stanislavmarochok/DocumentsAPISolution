namespace DocumentsAPI.UseCases.Interfaces
{
    public interface IDeleteDocument
    {
        Task<bool?> HandleAsync(Guid documentId);
    }
}
