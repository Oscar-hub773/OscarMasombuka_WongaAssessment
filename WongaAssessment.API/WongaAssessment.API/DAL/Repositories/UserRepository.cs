using Microsoft.EntityFrameworkCore;
using WongaAssessment.API.Data.Configurations;
using WongaAssessment.API.Data.Repositories.Interface;
using WongaAssessment.API.Models.Domain;

namespace WongaAssessment.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
        }
        public async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<UserModel?> GetUserByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
