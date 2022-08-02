using Microsoft.AspNetCore.Authorization;

namespace VPMS.Api.PolicyRequirements.RoleClaimRequirements;

public sealed class RoleClaimRequirement : IAuthorizationRequirement
{
    public string[] ClaimValue { get; }

    public RoleClaimRequirement(params string[] claimValue)
    {
        ClaimValue = claimValue;
    }

}