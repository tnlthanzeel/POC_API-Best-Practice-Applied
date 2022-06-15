using AutoMapper;
using MediatR;

namespace POC.Application.Features.Schools.Command.CreateSchool
{
    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, CreateSchoolCommandResponse>
    {
        private readonly IMapper _mapper;


        public CreateSchoolCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<CreateSchoolCommandResponse> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
