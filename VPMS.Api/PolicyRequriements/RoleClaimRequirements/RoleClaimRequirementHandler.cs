using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VPMS.Domain.Entities.IdentityUserEntities;
using VPMS.Persistence;

namespace VPMS.Api.PolicyRequirements.RoleClaimRequirements;

public sealed class RoleClaimRequirementHandler : AuthorizationHandler<RoleClaimRequirement>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly VPMSDbContext _dbContext;
    private readonly RoleManager<UserRole> _roleManager;

    public RoleClaimRequirementHandler(UserManager<ApplicationUser> userManager, VPMSDbContext vPMSDbContext, RoleManager<UserRole> roleManager)
    {
        _userManager = userManager;
        _dbContext = vPMSDbContext;
        _roleManager = roleManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleClaimRequirement requirement)
    {
        if (!context.User.Identity?.IsAuthenticated ?? false)
        {
            context.Fail();
            return;
        }

        var userEmail = context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Email)?.Value;

        if (userEmail is null)
        {
            context.Fail();
            return;
        }

        var user = await _userManager.GetUserAsync(context.User);

        var userRoles = await _userManager.GetRolesAsync(user);

        var userRoleIds = await _roleManager.Roles.Where(w => userRoles.Contains(w.Name)).Select(s => s.Id).ToListAsync();

        if (user is null)
        {
            context.Fail();
            return;
        }

        var hasClaim = await _dbContext.RoleClaims
                                       .AsNoTracking()
                                       .Where(w => userRoleIds.Contains(w.RoleId) && requirement.ClaimValue.Contains(w.ClaimValue))
                                       .AnyAsync();

        if (hasClaim is true)
        {
            context.Succeed(requirement);
        }

        else
        {
            context.Fail();
        }
    }
}
