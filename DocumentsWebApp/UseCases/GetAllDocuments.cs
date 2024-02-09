using DocumentsAPI.Models;
using DocumentsAPI.Services;
using DocumentsAPI.UseCases.Interfaces;

namespace DocumentsAPI.UseCases
{
    public class GetAllDocuments : IGetAllDocuments
    {
        private readonly IDocumentService documentService;

        public GetAllDocuments(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        public async IAsyncEnumerable<Document> HandleAsync()
        {
            var testDocuments = CreateTestDocuments();
            foreach (var document in testDocuments)
            {
                yield return document;
            }
        }

        private List<Document> CreateTestDocuments() => new List<Document>
        {
            new Document
            {
                Id = Guid.NewGuid(),
                ActualData = new Dictionary<string, string>
                {
                    {"key1", "data1" },
                    {"key2", "data2" },
                },
                Name = "test name",
                Tags = new List<string>
                {
                    "tag 1",
                    "tag 34"
                }
            },
            new Document
            {
                Id = Guid.NewGuid(),
                ActualData = "test data 3333",
                Name = "test name 45454",
                Tags = new List<string>
                {
                    "tag 345",
                    "jklj"
                }
            }
        };
    }
}
