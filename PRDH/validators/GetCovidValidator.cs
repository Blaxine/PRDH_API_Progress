using FluentValidation;
using PRDH.models;
using PRDH.models.requests;

namespace PRDH.validators
{
    public class GetCovidValidator : AbstractValidator<GetCovidFilters>
    {
        public GetCovidValidator() {
            RuleFor(c=> c.orderTestType).NotEmpty().WithMessage("Order testType cannot be null");
            RuleFor(c => c.sampleCollectedEndDate).NotEmpty().WithMessage("Collection Date cannot be null");
            RuleFor(c => c.createdAtEndDate).NotEmpty().WithMessage("Created at end date cannot be null");
            RuleFor(c => c.createdAtStartDate).NotEmpty().WithMessage("Created at start date cannot be null");
            RuleFor(c => c.sampleCollectedStartDate).NotEmpty().WithMessage("Collection start Date cannot be null");
        }

    }
}
