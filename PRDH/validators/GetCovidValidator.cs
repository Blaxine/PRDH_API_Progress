using FluentValidation;
using PRDH.models;
using PRDH.models.requests;

namespace PRDH.validators
{
    public class GetCovidValidator : AbstractValidator<GetCovidFilters>
    {
        public GetCovidValidator()
        {
            RuleFor(c => c.OrderTestType).NotEmpty().WithMessage("Order testType cannot be null");
            RuleFor(c => c.CreatedAtEndDate).NotEmpty().WithMessage("Created at end date cannot be null");
            RuleFor(c => c.CreatedAtStartDate).NotEmpty().WithMessage("Created at start date cannot be null");
        }

    }
}
