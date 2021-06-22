using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.Users.Command.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
