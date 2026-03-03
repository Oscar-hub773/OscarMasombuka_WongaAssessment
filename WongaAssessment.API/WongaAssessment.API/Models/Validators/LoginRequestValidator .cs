using FluentValidation;
using WongaAssessment.API.Models.DTOs.reguests;

namespace WongaAssessment.API.Models.Validators
{
    public class LoginRequestValidator:AbstractValidator<LoginRequestDTO>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
