using ChatApp.DTO.Authentication;
using ChatApp.DTO.Validators.BaseValidator;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class SignInDTOValidator : Validator<SignInDTO>
    {
        public SignInDTOValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required.")
                                    .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                    .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Date of birth is required.")
                .Must(dob => dob.HasValue && CalculateAge(dob.Value) >= 18)
                .WithMessage("You must be at least 18 years old to register.");
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;
        }
    }
}
