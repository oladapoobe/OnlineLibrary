using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OnlineLibrary.Application.Features.Users.Commands.CreateUser;
using Microsoft.Extensions.Logging;
using AutoMapper;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Application.Exceptions;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Test
{
    [TestFixture]
    public class CreateUserCommandHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<CreateUserCommandHandler>> _loggerMock;
        private CreateUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
            _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_UniqueUsername_ShouldCreateNewUser()
        {
            // Arrange
            var command = new CreateUserCommand { Username = "testuser", Password = "testpass" };
            var userEntity = new User { Username = command.Username }; // Add other necessary properties

            _userRepositoryMock.Setup(ur => ur.GetByUsernameAsync(command.Username)).ReturnsAsync((User)null);
            _mapperMock.Setup(m => m.Map<User>(It.IsAny<CreateUserCommand>())).Returns(userEntity);
            _userRepositoryMock.Setup(ur => ur.AddAsync(It.IsAny<User>())).ReturnsAsync(new User { Id = 1 }); // Assume User has Id property

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(true, result.Success);
            _userRepositoryMock.Verify(ur => ur.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task Handle_DuplicateUsername_ShouldThrowUserTakenMessage()
        {
            // Arrange
            var command = new CreateUserCommand { Username = "testuser", Password = "testpass" };
            var existingUser = new User { Username = command.Username }; // Add other necessary properties

            _userRepositoryMock.Setup(ur => ur.GetByUsernameAsync(command.Username)).ReturnsAsync(existingUser);

            // Act & Assert
            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(false, result.Success);
        }
    }
}
