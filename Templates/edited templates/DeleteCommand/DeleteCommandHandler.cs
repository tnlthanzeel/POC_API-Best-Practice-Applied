using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace $rootnamespace$.Delete$fileinputname$
{
    public class $safeitemname$ : IRequestHandler<Delete$fileinputname$Command>
    {
        public $safeitemname$ ()
        {

        }

        public async Task<Unit> Handle(Delete$fileinputname$Command request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
