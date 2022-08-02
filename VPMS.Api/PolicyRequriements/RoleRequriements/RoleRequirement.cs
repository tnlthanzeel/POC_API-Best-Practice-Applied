using Microsoft.AspNetCore.Authorization;

namespace VPMS.Api.PolicyRequirements.RoleRequriements;

public sealed class RoleRequirement : IAuthorizationRequirement
{
    public RoleRequirement() { }

}
