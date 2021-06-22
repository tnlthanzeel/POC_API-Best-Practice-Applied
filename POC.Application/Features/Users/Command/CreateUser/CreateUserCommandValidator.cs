﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Command.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");


            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");


            RuleFor(r => r.Address)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 900 characters.");


            RuleFor(r => r.School)
              .NotEmpty().WithMessage("{PropertyName} is required")
              .MaximumLength(50).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(r => r.Gender)
                .IsInEnum().NotEqual(Gender.None).WithName("Gender").WithMessage("{PropertyName} invalid gender");
        }
    }
}
