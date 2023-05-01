using ChatApp.DTO;
using ChatApp.DTO.Authentication;
using ChatApp.DTO.Validators.BaseValidator;
using FluentValidation;

namespace ChatApp.API.Validators
{
    public class AvatarDTOValidator : Validator<AvatarDTO>
    {
        public AvatarDTOValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("FileName is required.");

            RuleFor(x => x.ContentType).NotEmpty().WithMessage("ContentType is required.");

            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotNull().When(x => x.ChatId == null)
                .WithMessage("Either UserId or ChatId must be non-null.");

            RuleFor(x => x.ChatId)
                .Cascade(CascadeMode.Stop)
                .NotNull().When(x => x.UserId == null)
                .WithMessage("Either UserId or ChatId must be non-null.");
        }
    }
}
