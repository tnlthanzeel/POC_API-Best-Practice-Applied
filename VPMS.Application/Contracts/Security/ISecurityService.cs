using VPMS.Application.Security.Dtos;

namespace VPMS.Application.Contracts.Security;

public interface ISecurityService
{
    Task<ResponseResult<AuthenticatedUserDto>> AuthenticateUser(AuthenticateUserDto model, CancellationToken token);
    Task<ResponseResult<UserDto>> CreateUser(UserCreateDto model, CancellationToken token);
    Task<ResponseResult<UserDto>> Getuser(Guid id, CancellationToken token);
}
