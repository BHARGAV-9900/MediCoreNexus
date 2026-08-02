using FluentValidation;

namespace MediCore.Application.Features.Patients.Queries.GetPagedPatients;

public class GetPagedPatientsQueryValidator
    : AbstractValidator<GetPagedPatientsQuery>
{
    public GetPagedPatientsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}