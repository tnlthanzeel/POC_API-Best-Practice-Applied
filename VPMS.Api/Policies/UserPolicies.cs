using Microsoft.AspNetCore.Authorization;
using VPMS.Api.PolicyRequirements.UserClaimRequirements;
using VPMS.Domain.AuthPolicies;
using VPMS.Domain.Permissions;

namespace VPMS.Api.Policies
{
    internal sealed class UserPolicies
    {
        public static void Apply(AuthorizationOptions options)
        {
            options.AddPolicy(UserAuthPolicy.Create,
                            policy =>
                            {
                                policy.Requirements.Add(new UserClaimRequirement(UserPermissions.Create));
                            });

            options.AddPolicy(UserAuthPolicy.View,
                            policy =>
                            {
                                policy.Requirements.Add(new UserClaimRequirement(UserPermissions.View));
                            });
        }
    }
}
