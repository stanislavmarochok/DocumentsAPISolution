using DocumentsAPI.Models;
using DocumentsAPI.Services;
using DocumentsAPI.UseCases;
using DocumentsAPI.UseCases.Interfaces;
using Moq;
using NUnit.Framework;

namespace Tests.UseCases
{
    [TestFixture]
    internal class GetDocumentByIdTests
    {
        private Mock<IDocumentService> _documentServiceMock;
        private IGetDocumentById _testee;

        [SetUp]
        public void Setup()
        {
            _documentServiceMock = new Mock<IDocumentService>();

            _testee = new GetDocumentById(_documentServiceMock.Object);
        }

        [Test]
        public async Task HandleAsync_WhenDocumentNotExists_ThenReturnNull()
        {
            // Arrange
            var documentId = Guid.NewGuid();
            Document document = null;

            _documentServiceMock.Setup(x => x.GetByIdAsync(documentId)).ReturnsAsync(document);

            // Act
            var result = await _testee.HandleAsync(documentId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task HandleAsync_WhenDocumentExists_ThenReturnDocument()
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

            _documentServiceMock.Setup(x => x.GetByIdAsync(documentId)).ReturnsAsync(document);

            // Act
            var result = await _testee.HandleAsync(documentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(documentId));
        }
    }
}
