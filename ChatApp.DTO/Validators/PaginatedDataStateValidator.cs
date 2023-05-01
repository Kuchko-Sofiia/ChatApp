using ChatApp.DTO.Validators.BaseValidator;
using FluentValidation;

namespace ChatApp.DTO.Validators
{
    public class PaginatedDataStateDTOValidator<T> : Validator<PaginatedDataStateDTO<T>> where T : Enum
    {
        public PaginatedDataStateDTOValidator()
        {
            RuleFor(dto => dto.PageIndex).GreaterThanOrEqualTo(0);

            RuleFor(dto => dto.PageSize).GreaterThan(0);

            RuleFor(dto => dto.SortDirection).IsInEnum();

            RuleFor(dto => dto.SortProperty).IsInEnum();
        }
    }
}
