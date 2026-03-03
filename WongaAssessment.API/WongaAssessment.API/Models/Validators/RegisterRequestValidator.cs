using FluentValidation;
using WongaAssessment.API.Models.DTOs.reguests;

namespace WongaAssessment.API.Models.Validators
{
    public class RegisterRequestValidator:AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
