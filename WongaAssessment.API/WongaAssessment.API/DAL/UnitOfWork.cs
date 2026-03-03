using Microsoft.EntityFrameworkCore.Metadata;
using WongaAssessment.API.Data.Configurations;
using WongaAssessment.API.Data.Interfaces;
using WongaAssessment.API.Data.Repositories.Interface;

namespace WongaAssessment.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;     
            Users = userRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
