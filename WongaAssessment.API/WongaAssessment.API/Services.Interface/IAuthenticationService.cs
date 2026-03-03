using WongaAssessment.API.Models.DTOs.reguests;
using WongaAssessment.API.Models.DTOs.Response;

namespace WongaAssessment.API.Service.Interface
{
    public interface IAuthenticationService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request);
        Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request);
    }
}
