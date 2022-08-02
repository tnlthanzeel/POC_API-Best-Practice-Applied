namespace VPMS.Application.Security.Dtos;

public class UserClaimsDto
{
    public int ClaimId { get; init; }

    public Guid UserId { get; init; }

    public string ClaimType { get; init; } = null!;

    public string ClaimValue { get; init; } = null!;
}
