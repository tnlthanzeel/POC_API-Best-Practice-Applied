using MediatR;

namespace POC.Application.Features.Users.Command.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
