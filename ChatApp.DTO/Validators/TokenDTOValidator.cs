using ChatApp.DTO.Authentication;
using ChatApp.DTO.Validators.BaseValidator;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class TokenDTOValidator : Validator<TokenDTO>
    {
        public TokenDTOValidator()
        {
            RuleFor(dto => dto.AccessToken).NotNull().NotEmpty();

            RuleFor(dto => dto.RefreshToken).NotNull().NotEmpty();
        }
    }
}
