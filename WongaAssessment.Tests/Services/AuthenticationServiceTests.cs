using FluentAssertions;
using Moq;
using WongaAssessment.API.Configurations.Security;
using WongaAssessment.API.Data.Interfaces;
using WongaAssessment.API.Data.Repositories.Interface;
using WongaAssessment.API.Exceptions;
using WongaAssessment.API.Models.Domain;
using WongaAssessment.API.Models.DTOs.reguests;
using WongaAssessment.API.Models.DTOs.Response;
using WongaAssessment.API.Service.Interface;
using WongaAssessment.API.Services;
using Xunit;

namespace WongaAssessment.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
        private readonly Mock<IJwtService> _jwtServiceMock = new();

        private readonly AuthenticationService _service;

        public AuthenticationServiceTests()
        {
            _unitOfWorkMock.Setup(u => u.Users)
                .Returns(_userRepoMock.Object);

            _service = new AuthenticationService(
                _unitOfWorkMock.Object,
                _passwordHasherMock.Object,
                _jwtServiceMock.Object
            );
        }
        [Fact]
        public async Task RegisterAsync_Should_Create_User_When_Email_Does_Not_Exist()
        {
            // Arrange
            var request = new RegisterRequestDTO
            {
                FirstName = "Oscar",
                LastName = "Masombuka",
                Email = "oscar@test.com",
                Password = "Password1!"
            };

            _userRepoMock
                .Setup(x => x.GetUserByEmailAsync(request.Email))
                .ReturnsAsync((UserModel?)null);

            _passwordHasherMock
                .Setup(x => x.Hash(request.Password))
                .Returns("hashedPassword");

            _jwtServiceMock
                .Setup(x => x.CreateJwtToken(It.IsAny<Guid>(), request.Email))
                .Returns("fake-jwt-token");

            // Act
            var result = await _service.RegisterAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(request.Email);
            result.Token.Should().Be("fake-jwt-token");

            _userRepoMock.Verify(x => x.AddUserAsync(It.IsAny<UserModel>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task RegisterAsync_Should_Throw_When_User_Already_Exists()
        {
            // Arrange
            var request = new RegisterRequestDTO
            {
                FirstName = "Oscar",
                LastName = "Masombuka",
                Email = "existing@test.com",
                Password = "Password1!"
            };

            var existingUser = new UserModel
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                FirstName = "Existing",
                LastName = "User",
                Password = "hashed"
            };

            _userRepoMock
                .Setup(x => x.GetUserByEmailAsync(request.Email))
                .ReturnsAsync(existingUser);

            // Act
            Func<Task> act = async () => await _service.RegisterAsync(request);

            // Assert
            await act.Should().ThrowAsync<BadRequestException>()
                .WithMessage("User already exists.");
        }
        [Fact]
        public async Task LoginAsync_Should_Return_Token_When_Credentials_Are_Correct()
        {
            // Arrange
            var request = new LoginRequestDTO
            {
                Email = "oscar@test.com",
                Password = "Password1!"
            };

            var user = new UserModel
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                Password = "hashedPassword"
            };

            _userRepoMock
                .Setup(x => x.GetUserByEmailAsync(request.Email))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(x => x.Verify(request.Password, user.Password))
                .Returns(true);

            _jwtServiceMock
                .Setup(x => x.CreateJwtToken(user.UserId, user.Email))
                .Returns("fake-jwt-token");

            // Act
            var result = await _service.LoginAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(user.Email);
            result.Token.Should().Be("fake-jwt-token");

            _passwordHasherMock.Verify(x => x.Verify(request.Password, user.Password), Times.Once);
            _jwtServiceMock.Verify(x => x.CreateJwtToken(user.UserId, user.Email), Times.Once);
        }
        [Fact]
        public async Task LoginAsync_Should_Throw_When_User_Not_Found()
        {
            // Arrange
            var request = new LoginRequestDTO
            {
                Email = "missing@test.com",
                Password = "Password1!"
            };

            _userRepoMock
                .Setup(x => x.GetUserByEmailAsync(request.Email))
                .ReturnsAsync((UserModel?)null);

            // Act
            Func<Task> act = async () => await _service.LoginAsync(request);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("User not found.");
        }
        [Fact]
        public async Task LoginAsync_Should_Throw_When_Password_Is_Incorrect()
        {
            // Arrange
            var request = new LoginRequestDTO
            {
                Email = "oscar@test.com",
                Password = "WrongPassword!"
            };

            var user = new UserModel
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                Password = "hashedPassword"
            };

            _userRepoMock
                .Setup(x => x.GetUserByEmailAsync(request.Email))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(x => x.Verify(request.Password, user.Password))
                .Returns(false);

            // Act
            Func<Task> act = async () => await _service.LoginAsync(request);

            // Assert
            await act.Should().ThrowAsync<BadRequestException>()
                .WithMessage("incorrect password provided.");
        }

    }
}