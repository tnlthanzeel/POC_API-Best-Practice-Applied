using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.Schools.Command.UpdateSchool
{
    public class UpdateSchoolCommandValidator : AbstractValidator<UpdateSchoolCommand>
    {
        public UpdateSchoolCommandValidator()
        {

        }
    }
}
