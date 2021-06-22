using MediatR;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Features.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<SuccessResponse<UserDetailViewModel>>
    {
        public Guid UserId { get; set; }
    }
}
