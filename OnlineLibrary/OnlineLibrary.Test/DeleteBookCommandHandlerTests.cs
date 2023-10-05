using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OnlineLibrary.Application.Features.Books.Commands.DeleteBook;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Domain; // Ensure you have the right namespace for Book and EntityBase
using AutoMapper;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Application.Exceptions;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Test
{
    [TestFixture]
    public class DeleteBookCommandHandlerTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<DeleteBookCommandHandler>> _loggerMock;
        private DeleteBookCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<DeleteBookCommandHandler>>();
            _handler = new DeleteBookCommandHandler(_bookRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ExistingBook_ShouldDeleteBook()
        {
            // Arrange
            var command = new DeleteBookCommand { Id = 1 };
            var bookToDelete = new Book
            {
                Name = "Test Book",
                Author = "Test Author",
                Publisher = "Test Publisher"
            };

            _bookRepositoryMock.Setup(br => br.GetByIdAsync(command.Id)).ReturnsAsync(bookToDelete);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _bookRepositoryMock.Verify(br => br.DeleteAsync(It.Is<Book>(b => b == bookToDelete)), Times.Once);
        }

        [Test]
        public void Handle_NonExistingBook_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteBookCommand { Id = 1 };

            _bookRepositoryMock.Setup(br => br.GetByIdAsync(command.Id)).ReturnsAsync((Book)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
