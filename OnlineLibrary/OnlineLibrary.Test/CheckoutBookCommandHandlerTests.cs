using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using OnlineLibrary.Application.Features.Books.Commands.CheckoutBook;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Application.Contracts.Persistence;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Collections.Generic;
using System.Linq.Expressions;
using OnlineLibrary.Application.Exceptions;

namespace OnlineLibrary.Test
{
    [TestFixture]
    public class CheckoutBookCommandHandlerTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<CheckoutBookCommandHandler>> _loggerMock;
        private CheckoutBookCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<CheckoutBookCommandHandler>>();

            _handler = new CheckoutBookCommandHandler(
                _bookRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task Handle_ValidCommand_BookIsSuccessfullyCreated()
        {
            // Arrange
            var command = new CheckoutBookCommand
            {
                Name = "Test Book",
                Author = "Test Author",
                Publisher = "Test Publisher",
                CreatedBy = "Test User",
                CreatedDate = DateTime.UtcNow
            };
            var bookEntity = new Book
            {
                Name = command.Name,
                Author = command.Author,
                Publisher = command.Publisher,
                CreatedBy = command.CreatedBy,
                CreatedDate = command.CreatedDate
            };

            _bookRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(new List<Book>());
            _mapperMock.Setup(mapper => mapper.Map<Book>(command))
                .Returns(bookEntity);
            _bookRepositoryMock.Setup(repo => repo.AddAsync(bookEntity))
                .ReturnsAsync(bookEntity);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(true, result.Success);
            _bookRepositoryMock.Verify(repo => repo.AddAsync(bookEntity), Times.Once);
        }

        [Test]
        public async Task Handle_BookAlreadyExists_ShouldThrowBookTakenMessage()
        {
           
                // Arrange
                var command = new CheckoutBookCommand { Name = "Existing Book" };
                var existingBooks = new List<Book> { new Book { Name = "Existing Book" } };

                _bookRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                    .ReturnsAsync((Expression<Func<Book, bool>> predicate) =>
                        existingBooks.AsReadOnly().Where(predicate.Compile()).ToList());

            // Act & Assert
            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(false, result.Success);
          
            

        }

        // Add more test cases for different scenarios
    }
}