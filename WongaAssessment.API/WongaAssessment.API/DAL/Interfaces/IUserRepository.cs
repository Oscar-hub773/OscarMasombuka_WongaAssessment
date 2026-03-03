using WongaAssessment.API.Models.Domain;

namespace WongaAssessment.API.Data.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserModel user);
        Task<UserModel?> GetUserByIdAsync(Guid userId);
    }
}



