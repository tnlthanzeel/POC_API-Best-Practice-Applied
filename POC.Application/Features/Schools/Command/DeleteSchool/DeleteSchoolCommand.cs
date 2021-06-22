using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.Schools.Command.DeleteSchool
{
    public class DeleteSchoolCommand : IRequest
    {
        public string Id { get; set; }
    }
}
