using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WongaAssessment.API.Configurations.Security;
using WongaAssessment.API.Data.Configurations;
using WongaAssessment.API.Data.Interfaces;
using WongaAssessment.API.Data.Repositories.Interface;
using WongaAssessment.API.Exceptions;
using WongaAssessment.API.Models.Domain;
using WongaAssessment.API.Models.DTOs.reguests;
using WongaAssessment.API.Models.DTOs.Response;
using WongaAssessment.API.Service.Interface;

namespace WongaAssessment.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        public AuthenticationService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);

            if (user == null)throw new NotFoundException("User not found.");

            var isValidPassword = _passwordHasher.Verify(request.Password,user.Password);

            if (!isValidPassword)throw new BadRequestException("incorrect password provided.");

            var token = _jwtService.CreateJwtToken(user.UserId, user.Email);

            return new AuthResponseDTO
            {
                Email = user.Email,
                Token = token
            };
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            var existingUser = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);

            if (existingUser != null)throw new BadRequestException("User already exists.");

            var user = new UserModel
            {
                UserId = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = _passwordHasher.Hash(request.Password),
                CreatedDate = DateTime.UtcNow,
            };

            await _unitOfWork.Users.AddUserAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = _jwtService.CreateJwtToken(user.UserId, user.Email);

            return new AuthResponseDTO
            {
                Email = user.Email,
                Token = token
            };
        }
    }
}
