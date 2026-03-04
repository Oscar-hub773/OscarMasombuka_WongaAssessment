using Xunit;
using Moq;
using FluentAssertions;
using WongaAssessment.API.Service;
using WongaAssessment.API.Data.Interfaces;
using WongaAssessment.API.Data.Repositories.Interface;
using WongaAssessment.API.Models.Domain;
using WongaAssessment.API.Exceptions;

namespace WongaAssessment.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();

        private readonly UserService _service;

        public UserServiceTests()
        {
            _unitOfWorkMock.Setup(u => u.Users)
                .Returns(_userRepoMock.Object);

            _service = new UserService(_unitOfWorkMock.Object);
        }
        [Fact]
        public async Task GetUserDetailsAsync_Should_Return_User_When_User_Exists()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var user = new UserModel
            {
                UserId = userId,
                FirstName = "Oscar",
                LastName = "Masombuka",
                Email = "oscar@test.com"
            };

            _userRepoMock
                .Setup(x => x.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _service.GetUserDetailsAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Oscar");
            result.LastName.Should().Be("Masombuka");
            result.Email.Should().Be("oscar@test.com");
        }
    }
}