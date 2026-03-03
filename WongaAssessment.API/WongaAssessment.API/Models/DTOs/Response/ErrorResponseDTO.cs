namespace WongaAssessment.API.Models.DTOs.Response
{
    public class ErrorResponseDTO
    {
        public string Message { get; set; } = null!;
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
