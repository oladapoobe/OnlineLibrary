using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Application.Features.Books.Queries.GetBooksList;
using OnlineLibrary.Domain; // Update to your actual namespace
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Test
{
    [TestFixture]
    public class GetBooksListQueryHandlerTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private GetBooksListQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetBooksListQueryHandler(_bookRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldReturnBooksList()
        {
            // Arrange
            // Arrange
            var request = new GetBooksListQuery("Test Name", "Test Author", "Test Publisher");
            // ... rest of the test
            var bookList = new List<Book>
            {
                new Book { Name = "Test Book 1", Author = "Test Author", Publisher = "Test Publisher" },
                new Book { Name = "Test Book 2", Author = "Test Author", Publisher = "Test Publisher" }
            };
            var booksVmList = new List<BooksVm>
            {
                new BooksVm { Name = "Test Book 1", Author = "Test Author", Publisher = "Test Publisher" },
                new BooksVm { Name = "Test Book 2", Author = "Test Author", Publisher = "Test Publisher" }
            };

            _bookRepositoryMock.Setup(br => br.GetBooksByCriteria(request.Author, request.Publisher, request.Name)).ReturnsAsync(bookList);
            _mapperMock.Setup(m => m.Map<List<BooksVm>>(It.IsAny<List<Book>>())).Returns(booksVmList);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.AreEqual(booksVmList, result);
            _bookRepositoryMock.Verify(br => br.GetBooksByCriteria(request.Author, request.Publisher, request.Name), Times.Once);
            _mapperMock.Verify(m => m.Map<List<BooksVm>>(It.IsAny<List<Book>>()), Times.Once);
        }
    }
}
