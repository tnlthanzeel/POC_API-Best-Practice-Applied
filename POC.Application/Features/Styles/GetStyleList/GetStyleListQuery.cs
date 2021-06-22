using MediatR;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.Styles.GetStyleList
{
    public class GetStyleListQuery : IRequest<SuccessResponse<IEnumerable<StyleListViewModel>>>
    {
    }
}
