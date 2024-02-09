using DocumentsAPI.Models;
using DocumentsAPI.Services;
using DocumentsAPI.UseCases.Interfaces;

namespace DocumentsAPI.UseCases
{
    public class UpdateDocument : IUpdateDocument
    {
        private readonly IDocumentService documentService;

        public UpdateDocument(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        public async Task<Document> HandleAsync(Guid documentId, Document newDocument)
        {
            var document = await documentService.UpdateAsync(documentId, newDocument);
            return document;
        }
    }
}
