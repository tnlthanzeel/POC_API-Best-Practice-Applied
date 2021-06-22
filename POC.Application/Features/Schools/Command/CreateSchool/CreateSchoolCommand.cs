using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.Schools.Command.CreateSchool
{
    public class CreateSchoolCommand : IRequest<CreateSchoolCommandResponse>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
