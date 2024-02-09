using DocumentsAPI.Models;
using DocumentsAPI.Services;
using DocumentsAPI.UseCases.Interfaces;

namespace DocumentsAPI.UseCases
{
    public class GetDocumentById : IGetDocumentById
    {
        private readonly IDocumentService documentService;

        public GetDocumentById(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        public async Task<Document> HandleAsync(Guid documentId)
        {
            var document = await documentService.GetByIdAsync(documentId);
            return document;
        }
    }
}
