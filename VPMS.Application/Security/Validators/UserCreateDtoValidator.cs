using FluentValidation;
using VPMS.Application.Security.Dtos;

namespace VPMS.Application.Security.Validators;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address")
            .MaximumLength(256).WithMessage("Email address must be less than 256 characters");

        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(256).WithMessage("Username must be less than 256 characters");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
