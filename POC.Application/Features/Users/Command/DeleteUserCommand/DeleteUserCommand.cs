using MediatR;
using POC.Application.Responses;

namespace POC.Application.Features.Users.Command.DeleteUserCommand;

public class DeleteUserCommand : IRequest<ResponseResult<Unit>>
{
    public Guid Id { get; set; }
}
