using System.Security.Claims;
using VPMS.Application.Security.Dtos;
using VPMS.Domain.Entities.IdentityUserEntities;

namespace VPMS.Application.PersistanceInterfaces;

public interface IUserSecurityRespository
{
    Task<IReadOnlyList<UserClaimsDto>> GetUserClaims(Guid userId, CancellationToken token);

    Task<IList<string>> GetUserRoles(Guid userId, CancellationToken token = default);
    Task<IReadOnlyList<UserRole>> GetAllRoles(CancellationToken token = default);
    Task<bool> HasClaim(Guid userId, IEnumerable<string> claimValue, CancellationToken token = default);
    Task<ApplicationUser?> GetUser(ClaimsPrincipal user);
    Task<ApplicationUser?> FindByEmail(string email);
    Task<ResponseResult<UserDto>> CreateUser(string userName, string password, string email, CancellationToken token);
    Task<ApplicationUser?> GetUser(Guid id);
}
