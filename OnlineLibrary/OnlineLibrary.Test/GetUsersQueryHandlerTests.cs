using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using AutoMapper;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Application.Exceptions;
using OnlineLibrary.Application.Features.Users.Queries.ValidateUser;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Test
{
    [TestFixture]
    public class GetUsersQueryHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IJwtTokenHandler> _tokenHandlerMock;
        private GetUsersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _tokenHandlerMock = new Mock<IJwtTokenHandler>();
            _handler = new GetUsersQueryHandler(_userRepositoryMock.Object, _mapperMock.Object, _tokenHandlerMock.Object);
        }

        [Test]
        public async Task Handle_ValidCredentials_ShouldReturnUserWithToken()
        {
            // Arrange
            var query = new GetUsersQuery("testuser", "testpass"); // Using constructor
            var user = new User { Username = query.Username, PasswordHash = BCrypt.Net.BCrypt.HashPassword(query.Password) };
            var userVm = new UsersVm { Username = user.Username };
            var token = "testtoken";

            _userRepositoryMock.Setup(ur => ur.GetByUsernameAsync(query.Username)).ReturnsAsync(user);
            _tokenHandlerMock.Setup(th => th.GenerateToken(user)).Returns(token);
            _mapperMock.Setup(m => m.Map<UsersVm>(user)).Returns(userVm);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.AreEqual(userVm.Username, result.Username);
            Assert.AreEqual(token, result.token);
        }

        [Test]
        public async Task Handle_InvalidCredentials_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var query = new GetUsersQuery("testuser", "wrongpass"); // Using constructor
            var user = new User { Username = query.Username, PasswordHash = BCrypt.Net.BCrypt.HashPassword("testpass") };

            _userRepositoryMock.Setup(ur => ur.GetByUsernameAsync(query.Username)).ReturnsAsync(user);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(query, CancellationToken.None));
        }

    }
}
