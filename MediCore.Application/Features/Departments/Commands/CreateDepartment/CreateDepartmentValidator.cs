using FluentValidation;

namespace MediCore.Application.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Department name is required.")
            .MaximumLength(100)
            .WithMessage("Department name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");
    }
}