using ChatApp.DTO.Validators.BaseValidator;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class ChatDTOValidator : Validator<ChatDTO>
    {
        public ChatDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.MembersCount).GreaterThanOrEqualTo(1).WithMessage("MembersCount must be greater than or equal to 1.");

            RuleFor(x => x.MembersId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("MembersId must not be null.")
                .ForEach(memberIdRule => memberIdRule.NotEmpty().WithMessage("MemberId must not be empty."));
        }
    }
}
