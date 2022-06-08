using FluentValidation;

namespace POC.Application.Features.Users.Command.CreateUser;

public class GradeValidator : AbstractValidator<Grade>
{
    public GradeValidator()
    {
        RuleFor(r => r.Name)
           .NotEmpty().WithMessage("invalid grade name")
           .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters."); ;
    }
}
