using System.Collections.Generic;

namespace VPMS.Application.Security.Dtos;

public class AuthenticatedUserDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = null!;
    public string BearerToken { get; init; } = null!;
    public bool IsAuthenticated { get; init; }
    public IReadOnlyList<UserClaimsDto> Claims { get; init; } = new List<UserClaimsDto>();
}
