using DocumentsAPI.Models;
using DocumentsAPI.Services;
using DocumentsAPI.UseCases.Interfaces;

namespace DocumentsAPI.UseCases
{
    public class CreateDocument : ICreateDocument
    {
        private readonly IDocumentService documentService;

        public CreateDocument(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        public async Task<Document> HandleAsync(Document newDocument)
        {
            var document = await documentService.CreateAsync(newDocument);
            return document;
        }
    }
}
