using MediatR;
using POC.Application.Responses;
using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Command.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Address, string School, Gender Gender, Grade Grade) : IRequest<ResponseResult<CreateUserCommandResponse>>;

public record Grade(string Name);

