using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OnlineLibrary.Application.Features.Books.Commands.UpdateBook;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Domain; // Ensure you have the correct namespace for Book and EntityBase
using AutoMapper;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Application.Exceptions;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Test
{
    [TestFixture]
    public class UpdateBookCommandHandlerTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<UpdateBookCommandHandler>> _loggerMock;
        private UpdateBookCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UpdateBookCommandHandler>>();
            _handler = new UpdateBookCommandHandler(_bookRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ExistingBook_ShouldUpdateBook()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1 }; // Assume you have an Id property and set other necessary properties on UpdateBookCommand
            var existingBook = new Book
            {
                Name = "Old Name",
                Author = "Old Author",
                Publisher = "Old Publisher"
            };

            _bookRepositoryMock.Setup(br => br.GetByIdAsync(command.Id)).ReturnsAsync(existingBook);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _bookRepositoryMock.Verify(br => br.UpdateAsync(It.Is<Book>(b => b == existingBook)), Times.Once);
            _mapperMock.Verify(m => m.Map(command, existingBook, typeof(UpdateBookCommand), typeof(Book)), Times.Once);
        }

        [Test]
        public void Handle_NonExistingBook_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1 };

            _bookRepositoryMock.Setup(br => br.GetByIdAsync(command.Id)).ReturnsAsync((Book)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
