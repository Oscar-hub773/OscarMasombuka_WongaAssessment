using WongaAssessment.API.Data.Configurations;
using WongaAssessment.API.Data.Interfaces;
using WongaAssessment.API.Data.Repositories.Interface;
using WongaAssessment.API.Exceptions;
using WongaAssessment.API.Models.DTOs.Response;
using WongaAssessment.API.Service.Interface;

namespace WongaAssessment.API.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserResponseDTO?> GetUserDetailsAsync(Guid userId)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(userId);

            if (user == null) throw new NotFoundException("User not found.");

            return new UserResponseDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}
