using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace $rootnamespace$.Get$fileinputname$
{
    public class $safeitemname$ : IRequestHandler<Get$fileinputname$Query, IEnumerable<$fileinputname$ViewModel>>
    {
         private readonly IMapper _mapper;

         public $safeitemname$ (IMapper mapper)
         {
            _mapper = mapper;
         }

         public async Task<IEnumerable<$fileinputname$ViewModel>> Handle(Get$fileinputname$Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
