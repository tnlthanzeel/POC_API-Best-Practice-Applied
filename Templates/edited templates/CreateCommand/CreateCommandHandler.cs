using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace $rootnamespace$.Create$fileinputname$
{
    public class $safeitemname$ : IRequestHandler<Create$fileinputname$Command, Create$fileinputname$CommandResponse>
    {
        private readonly IMapper _mapper;
        

        public $safeitemname$(IMapper mapper)
        {
            _mapper = mapper;
        }

         public async Task<Create$fileinputname$CommandResponse> Handle(Create$fileinputname$Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
