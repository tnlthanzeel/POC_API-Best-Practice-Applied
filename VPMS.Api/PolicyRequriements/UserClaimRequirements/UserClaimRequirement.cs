using Microsoft.AspNetCore.Authorization;

namespace VPMS.Api.PolicyRequirements.UserClaimRequirements
{
    public sealed class UserClaimRequirement : IAuthorizationRequirement
    {
        public string[] ClaimValue { get; }

        public UserClaimRequirement(params string[] claimValue)
        {
            ClaimValue = claimValue;
        }
    }
}
