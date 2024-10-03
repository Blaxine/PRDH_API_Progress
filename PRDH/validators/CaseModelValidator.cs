using FluentValidation;
using PRDH.models;

namespace PRDH.validators
{
    public class CaseModelValidator : AbstractValidator<CaseModel>
    {
        public CaseModelValidator() {
            RuleFor(c => c.caseCategory).NotEmpty().WithMessage("Case must have a category");
            RuleFor(c => c.caseId).NotNull().WithMessage("Provide a valid Case Id");
            RuleFor(c => c.caseType).NotNull().NotEmpty().WithMessage("case type must not be empty"); 
            RuleFor(c => c.caseClassification).NotNull().NotEmpty().WithMessage("Case classification must not be empty");
            RuleFor(c=> c.earliestPositiveDiagnosticTestSampleCollectedDate).NotNull().NotEmpty().WithMessage("Earliest Positive Diagnostic Test Sample Collected Date must not Be empty");
            RuleFor(c => c.earliestPositiveRankingTestSampleCollectedDate).NotNull().NotEmpty().WithMessage("Earliest Positive Ranking Test Sample Collected Date must not Be empty");

        }
    }
}
