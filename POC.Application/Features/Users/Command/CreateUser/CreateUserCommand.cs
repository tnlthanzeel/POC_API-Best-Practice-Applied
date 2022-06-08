using MediatR;
using POC.Application.Responses;
using System.ComponentModel.DataAnnotations;
using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Command.CreateUser
{
    public class CreateUserCommand : IRequest<ResponseResult<CreateUserCommandResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string School { get; set; }
        public Gender Gender { get; set; }
        public Grade Grade { get; set; } = new();
    }

    public class Grade
    {
        public string Name { get; set; }
    }
}
