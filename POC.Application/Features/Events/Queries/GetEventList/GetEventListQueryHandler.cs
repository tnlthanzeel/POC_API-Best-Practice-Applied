using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Application.Features.Events.Queries.GetEventList
{
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, IEnumerable<EventListViewModel>>
    {
        private readonly IMapper _mapper;

        public GetEventListQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventListViewModel>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
