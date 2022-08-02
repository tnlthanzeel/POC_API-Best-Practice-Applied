using VPMS.Domain.Entities.IdentityUserEntities;

namespace VPMS.Application.Contracts.Security;

public interface ITokenBuilder
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user, CancellationToken token);
}
