using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class ChatDTOValidator : AbstractValidator<ChatDTO>
    {
        public ChatDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.MembersCount).GreaterThanOrEqualTo(0).WithMessage("MembersCount must be non-negative.");
            RuleFor(x => x.MembersId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("MembersId must not be null.")
                .ForEach(memberIdRule => memberIdRule.NotEmpty().WithMessage("MemberId must not be empty."));
        }
    }
}
