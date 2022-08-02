using Microsoft.AspNetCore.Authorization;
using VPMS.Api.PolicyRequirements.UserClaimRequirements;
using VPMS.Domain.AuthPolicies;
using VPMS.Domain.Permissions;

namespace VPMS.Api.Policies;

internal sealed class TodoItemPolicies
{
    public static void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(TodoItemAuthPolicy.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(TodoItemPermissions.View));
                        });

        options.AddPolicy(TodoItemAuthPolicy.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(TodoItemPermissions.Create));
                        });
    }
}
