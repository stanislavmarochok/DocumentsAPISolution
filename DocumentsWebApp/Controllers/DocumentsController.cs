using DocumentsAPI.Models;
using DocumentsAPI.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IGetAllDocuments getAllDocuments;
        private readonly IGetDocumentById getDocumentById;
        private readonly ICreateDocument createDocument;
        private readonly IUpdateDocument updateDocument;
        private readonly IDeleteDocument deleteDocument;

        public DocumentsController(
            IGetAllDocuments getAllDocuments,
            IGetDocumentById getDocumentById,
            ICreateDocument createDocument,
            IUpdateDocument updateDocument,
            IDeleteDocument deleteDocument)
        {
            this.getAllDocuments = getAllDocuments;
            this.getDocumentById = getDocumentById;
            this.createDocument = createDocument;
            this.updateDocument = updateDocument;
            this.deleteDocument = deleteDocument;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetDocuments()
        {
            var documents = await getAllDocuments.HandleAsync().ToListAsync();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(Guid id)
        {
            var document = await getDocumentById.HandleAsync(id);
            if (document == null)
                return NotFound();

            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] Document document)
        {
            var createdDocument = await createDocument.HandleAsync(document);
            return CreatedAtAction(nameof(GetDocument), new { id = createdDocument.Id }, createdDocument);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] Document updatedDocument)
        {
            var document = await getDocumentById.HandleAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            var result = await updateDocument.HandleAsync(id, updatedDocument);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var result = await deleteDocument.HandleAsync(id);

            if (result == null)
                return NotFound("Document was not found");

            return Ok(result);
        }
    }
}
