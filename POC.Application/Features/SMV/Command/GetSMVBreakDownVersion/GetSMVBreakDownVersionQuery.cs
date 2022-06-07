using MediatR;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.SMV.Command.GetSMVBreakDownVersion
{
    public class GetSMVBreakDownVersionQuery : IRequest<ResponseResult<SMVBreakDownVersionViewModel>>
    {
        public string VersionHDID { get; set; }
    }
}
