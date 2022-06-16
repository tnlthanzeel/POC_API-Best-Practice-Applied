using MediatR;
using POC.Application.Responses;
using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Command.UpdateUser;

public record UpdateUserCommand : IRequest<ResponseResult<Unit>>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string School { get; set; } = null!;
    public Gender Gender { get; set; }
}
