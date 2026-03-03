using System.ComponentModel.DataAnnotations;

namespace WongaAssessment.API.Models.Domain
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
}



