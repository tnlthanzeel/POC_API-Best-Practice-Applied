using MediatR;

namespace POC.Application.Features.Schools.Command.DeleteSchool;

public class DeleteSchoolCommandHandler : IRequestHandler<DeleteSchoolCommand>
{
    public DeleteSchoolCommandHandler()
    {

    }

    public async Task<Unit> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Unit.Value);
    }
}
