namespace WongaAssessment.API.Models.DTOs.Response
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
