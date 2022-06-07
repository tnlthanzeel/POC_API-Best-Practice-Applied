using MediatR;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.Garment.GetGarmentTypeList
{
    public class GetGarmentTypeListQuery : IRequest<ResponseResult<IEnumerable<GarmentTypeListViewModel>>>
    {
    }
}
