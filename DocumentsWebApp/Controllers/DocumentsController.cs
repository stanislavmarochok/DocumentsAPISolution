using DocumentsAPI.Models;
using DocumentsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetDocuments()
        {
            var documents = new List<Document>
            {
                new Document
                {
                    Id = Guid.NewGuid(),
                    Data = new Dictionary<string, string>
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
                    Data = "test data 3333",
                    Name = "test name 45454",
                    Tags = new List<string>
                    {
                        "tag 345",
                        "jklj"
                    }
                }
            };

            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(Guid id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
                return NotFound();

            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] Document document)
        {
            var createdDocument = await _documentService.CreateAsync(document);
            return CreatedAtAction(nameof(GetDocument), new { id = createdDocument.Id }, createdDocument);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] Document updatedDocument)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            await _documentService.UpdateAsync(id, updatedDocument);
            return NoContent();
        }

        // PUT and other methods can be added here following the same pattern
    }
}
