using DocumentsAPI.Services;
using DocumentsAPI.UseCases.Interfaces;

namespace DocumentsAPI.UseCases
{
    public class DeleteDocument : IDeleteDocument
    {
        private readonly IDocumentService documentService;

        public DeleteDocument(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        public async Task<bool?> HandleAsync(Guid documentId)
        {
            var documentExists = await documentService.GetByIdAsync(documentId);
            if (documentExists == null)
                return null;

            var document = await documentService.DeleteAsync(documentId);
            return document;
        }
    }
}
