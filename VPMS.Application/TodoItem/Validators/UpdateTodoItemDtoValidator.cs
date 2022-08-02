using FluentValidation;
using VPMS.Application.TodoItem.Dtos;

namespace VPMS.Application.TodoItem.Validators;

public class UpdateTodoItemDtoValidator : AbstractValidator<UpdateTodoItemDto>
{
    public UpdateTodoItemDtoValidator()
    {
        RuleFor(r => r.Title)
       .NotEmpty().WithMessage("{PropertyName} is required")
       .MaximumLength(250).WithMessage("{PropertyName} must be less than 250 characters");

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(500).WithMessage("{PropertyName} must be less than 500 characters");
    }
}
