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
      
        }

    }
}
