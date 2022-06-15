using MediatR;
using POC.Application.Responses;

namespace POC.Application.Features.Users.Queries.GetUserList;

public class GetUsersListQuery : IRequest<ResponseResult<IEnumerable<UserViewModel>>>
{
}
