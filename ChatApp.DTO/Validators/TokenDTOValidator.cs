using ChatApp.DTO.Authentication;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class TokenDTOValidator : AbstractValidator<TokenDTO>
    {
        public TokenDTOValidator()
        {
            RuleFor(dto => dto.AccessToken).NotNull().NotEmpty();
            RuleFor(dto => dto.RefreshToken).NotNull().NotEmpty();
        }
    }
}
