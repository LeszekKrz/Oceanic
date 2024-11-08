using Moq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Xunit;
using RoutePlanning.Domain.Users;
using RoutePlanning.Application.Users.Queries.AuthenticatedUser;
using RoutePlanning.Infrastructure.Repositories;
using Netcompany.Net.DomainDrivenDesign.Models;
using RoutePlanning.Application.Users.Queries.GetAllUsers;

namespace RoutePlanning.Domain.UnitTests.User
{
    public sealed class UserTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AuthenticatedUserQueryHandler _authenticatedUserHandler;
        private readonly GetAllUsersQueryHandler _allUsersQueryHandler;

        public UserTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authenticatedUserHandler = new AuthenticatedUserQueryHandler(_userRepositoryMock.Object);
            _allUsersQueryHandler = new GetAllUsersQueryHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCredentials_ReturnsAuthenticatedUser()
        {
            // Arrange
            var username = "admin";
            var password = "admin";
            var hashedPassword = Users.User.ComputePasswordHash(password);

            var expectedUsername = username;
            var expectedIsAdmin = true; // Modify as needed for test case

            _userRepositoryMock
                .Setup(repo => repo.getAuthenticatedUser(username, hashedPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthenticatedUser(new Entity<Users.User, Guid>.EntityId(), expectedUsername, expectedIsAdmin));

            var query = new AuthenticatedUserQuery(username, password);

            // Act
            var result = await _authenticatedUserHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsername, result.Username);
            //Assert.Equal(expectedIsAdmin, result.isAdmin);

            _userRepositoryMock.Verify(repo => repo.getAuthenticatedUser(username, hashedPassword, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var username = "admin";
            var invalidPassword = "wrongpassword";
            var hashedPassword = Users.User.ComputePasswordHash(invalidPassword);

            _userRepositoryMock
                .Setup(repo => repo.getAuthenticatedUser(username, hashedPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthenticatedUser?)null);

            var query = new AuthenticatedUserQuery(username, invalidPassword);

            // Act
            var result = await _authenticatedUserHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _userRepositoryMock.Verify(repo => repo.getAuthenticatedUser(username, hashedPassword, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsNull()
        {
            // Arrange
            var username = "nonexistentuser";
            var password = "password";
            var hashedPassword = Users.User.ComputePasswordHash(password);

            _userRepositoryMock
                .Setup(repo => repo.getAuthenticatedUser(username, hashedPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthenticatedUser?)null);

            var query = new AuthenticatedUserQuery(username, password);

            // Act
            var result = await _authenticatedUserHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _userRepositoryMock.Verify(repo => repo.getAuthenticatedUser(username, hashedPassword, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var username = "admin";
            var password = "admin";
            var hashedPassword = Users.User.ComputePasswordHash(password);

            _userRepositoryMock
                .Setup(repo => repo.getAuthenticatedUser(username, hashedPassword, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new System.Exception("Database error"));

            var query = new AuthenticatedUserQuery(username, password);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(async () => await _authenticatedUserHandler.Handle(query, CancellationToken.None));

            _userRepositoryMock.Verify(repo => repo.getAuthenticatedUser(username, hashedPassword, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
