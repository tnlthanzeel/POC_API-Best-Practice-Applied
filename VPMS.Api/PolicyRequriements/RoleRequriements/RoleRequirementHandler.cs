using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VPMS.Application.PersistanceInterfaces;
using VPMS.Domain.Entities.IdentityUserEntities;

namespace VPMS.Api.PolicyRequirements.RoleRequriements
{
    public sealed class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserSecurityRespository _userSecurityRespository;

        public RoleRequirementHandler(UserManager<ApplicationUser> userManager, IUserSecurityRespository userSecurityRespository)
        {
            _userManager = userManager;
            _userSecurityRespository = userSecurityRespository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
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

            var allRoles = await _userSecurityRespository.GetAllRoles();

            var user = await _userManager.GetUserAsync(context.User);

            var assignedRolesNamesForUser = await _userSecurityRespository.GetUserRoles(user.Id);

            var isInRole = allRoles.Any(a => assignedRolesNamesForUser.Contains(a.Name));

            if (isInRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
