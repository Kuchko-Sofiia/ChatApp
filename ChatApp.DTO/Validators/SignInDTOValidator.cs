using ChatApp.DTO.Authentication;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class SignInDTOValidator : AbstractValidator<SignInDTO>
    {
        public SignInDTOValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().When(x => string.IsNullOrEmpty(x.Email)).WithMessage("Username is required.")
                                 .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");
            RuleFor(x => x.Email).NotEmpty().When(x => string.IsNullOrEmpty(x.UserName)).WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("Invalid email address.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                    .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");
            RuleFor(x => x.DateOfBirth)
            .NotNull()
            .WithMessage("Date of birth is required.")
            .Must(dob => CalculateAge(dob) >= 18)
            .WithMessage("You must be at least 18 years old to register.");
        }

        private int CalculateAge(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                throw new ArgumentNullException(nameof(dateOfBirth));
            }

            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Value.Year;
            if (dateOfBirth.Value.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
