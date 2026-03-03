using WongaAssessment.API.Models.DTOs.Response;

namespace WongaAssessment.API.Service.Interface
{
    public interface IUserService
    {
        Task<UserResponseDTO?> GetUserDetailsAsync(Guid userId);
    }
}
