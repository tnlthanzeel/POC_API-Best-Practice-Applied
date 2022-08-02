using Microsoft.AspNetCore.Authorization;
using VPMS.Api.Policies;

namespace VPMS.Api.DIServiceExtensions;

internal static class AuthorizationPolicyConfigHelpers
{
    internal static void ApplyPolicies(AuthorizationOptions options)
    {
        TodoItemPolicies.Apply(options);

        UserPolicies.Apply(options);
    }
}