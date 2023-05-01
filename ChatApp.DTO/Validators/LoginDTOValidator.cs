using ChatApp.DTO.Authentication;
using ChatApp.DTO.Validators.BaseValidator;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class LoginDTOValidator : Validator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                      .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
