using AutoMapper;
using MediatR;

namespace POC.Application.Features.Schools.Command.UpdateSchool;

public class UpdateSchoolCommandHandler : IRequestHandler<UpdateSchoolCommand>
{
    private readonly IMapper _mapper;

    public UpdateSchoolCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Unit.Value);
    }
}
