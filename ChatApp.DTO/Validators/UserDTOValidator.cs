using ChatApp.API.Validators;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.").MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Email is not valid.");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");
            RuleFor(x => x.PhoneNumber).Matches(@"^\+?\d{1,3}[-. (]?\d{3}[-. )]?\d{3}[-. ]?\d{4}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber)).WithMessage("PhoneNumber is not valid.");
        }
    }
}
