namespace WongaAssessment.API.Configurations.Security
{
    public interface IJwtService
    {
        string CreateJwtToken(Guid userId, string email);
    }

}
