using MediatR;
using POC.Application.Responses;

namespace POC.Application.Features.Users.Queries.GetUserDetail;

public class GetUserDetailQuery : IRequest<ResponseResult<UserDetailViewModel>>
{
    public Guid UserId { get; set; }
}
