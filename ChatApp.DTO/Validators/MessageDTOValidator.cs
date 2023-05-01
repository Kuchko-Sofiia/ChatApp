using ChatApp.DTO.Validators.BaseValidator;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class MessageDTOValidator : Validator<MessageDTO>
    {
        public MessageDTOValidator()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Message text is required.")
                    .MaximumLength(2000).WithMessage("Message text cannot exceed 2000 characters.");

            RuleFor(x => x.ChatId).NotEmpty().WithMessage("Chat ID is required.");

            RuleFor(x => x.SenderId).NotEmpty().WithMessage("Sender ID is required.");

            RuleFor(x => x.SentTime).NotEmpty().WithMessage("Sent time is required.");
        }
    }
}
