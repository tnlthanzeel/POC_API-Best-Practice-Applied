using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace $rootnamespace$.Update$fileinputname$
{
    public class $safeitemname$ : IRequestHandler<Update$fileinputname$Command>
    {
        private readonly IMapper _mapper;

        public $safeitemname$(IMapper mapper)
        {
            _mapper = mapper;
        }

         public async Task<Unit> Handle(Update$fileinputname$Command request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
