using DocumentsAPI.Models;
using DocumentsAPI.Services;
using DocumentsAPI.UseCases;
using DocumentsAPI.UseCases.Interfaces;
using Moq;
using NUnit.Framework;

namespace Tests.UseCases
{
    [TestFixture]
    internal class UpdateDocumentTests
    {
        private Mock<IDocumentService> _documentServiceMock;
        private IUpdateDocument _testee;

        [SetUp]
        public void Setup()
        {
            _documentServiceMock = new Mock<IDocumentService>();

            _testee = new UpdateDocument(_documentServiceMock.Object);
        }

        [Test]
        public async Task HandleAsync_WhenDocumentNotExists_ThenReturnNull()
        {
            // Arrange
            var documentId = Guid.NewGuid();
            Document document = null;

            Document updatedDocument = new Document
            {
                Id = documentId,
                Name = "new updated name",
                ActualData = "new updated data",
                Tags = new List<string>
                {
                    "tag 1",
                    "tag 2"
                }
            };

            _documentServiceMock.Setup(x => x.UpdateAsync(documentId, It.IsAny<Document>())).ReturnsAsync(document);

            // Act
            var result = await _testee.HandleAsync(documentId, updatedDocument);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task HandleAsync_WhenDocumentExists_ThenUpdateDocument_AndReturnUpdatedDocument()
        {
            // Arrange
            var documentId = Guid.NewGuid();
            Document document = new Document
            {
                Id = documentId,
                Name = "test document",
                ActualData = new List<string>
                {
                    "test 1",
                    "test 2"
                },
                Tags = new List<string>
                {
                    "tag 1",
                    "tag 2"
                }
            };

            Document updatedDocument = new Document
            {
                Id = documentId,
                Name = "new updated name",
                ActualData = "new updated data",
                Tags = new List<string>
                {
                    "tag 4",
                    "tag 5"
                }
            };

            _documentServiceMock.Setup(x => x.UpdateAsync(documentId, It.IsAny<Document>())).ReturnsAsync(updatedDocument);

            // Act
            var result = await _testee.HandleAsync(documentId, updatedDocument);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(documentId));
            Assert.That(result.Name, Is.EqualTo("new updated name"));
            Assert.That(result.ActualData, Is.EqualTo("new updated data"));
            Assert.That(result.Tags[0], Is.EqualTo("tag 4"));
            Assert.That(result.Tags[1], Is.EqualTo("tag 5"));
        }
    }
}
