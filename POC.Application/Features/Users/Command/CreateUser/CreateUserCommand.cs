using MediatR;
using POC.Application.Responses;
using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Command.CreateUser;

//public class CreateUserCommand : IRequest<ResponseResult<CreateUserCommandResponse>>
//{
//    public string FirstName { get; set; } = null!;
//    public string LastName { get; set; } = null!;
//    public string Address { get; set; } = null!;
//    public string School { get; set; } = null!;
//    public Gender Gender { get; set; }
//    public Grade Grade { get; set; } = new();
//}

//public class Grade
//{
//    public string Name { get; set; } = null!;
//}

public record CreateUserCommand(string FirstName, string LastName, string Address, string School, Gender Gender, Grade Grade) : IRequest<ResponseResult<CreateUserCommandResponse>>;

public record Grade(string Name);

