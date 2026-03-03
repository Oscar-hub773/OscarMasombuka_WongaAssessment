using WongaAssessment.API.Data.Repositories.Interface;

namespace WongaAssessment.API.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
