using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.Schools.Command.CreateSchool
{
    public class CreateSchoolCommandValidator : AbstractValidator<CreateSchoolCommand>
    {
        public CreateSchoolCommandValidator()
        {

        }
    }
}
