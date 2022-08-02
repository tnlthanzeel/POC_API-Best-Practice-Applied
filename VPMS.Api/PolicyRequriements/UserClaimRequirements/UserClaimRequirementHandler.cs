using Microsoft.AspNetCore.Authorization;
using VPMS.Application.PersistanceInterfaces;
using VPMS.Domain.Permissions;
using VPMS.SharedKernel.Interfaces;

namespace VPMS.Api.PolicyRequirements.UserClaimRequirements;

public sealed class UserClaimRequirementHandler : AuthorizationHandler<UserClaimRequirement>
{
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IUserSecurityRespository _userSecurityRespository;

    public UserClaimRequirementHandler(ILoggedInUserService loggedInUserService, IUserSecurityRespository userSecurityRespository)
    {
        _loggedInUserService = loggedInUserService;
        _userSecurityRespository = userSecurityRespository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserClaimRequirement requirement)
    {
        if (context.User.Identity!.IsAuthenticated is false)
        {
            context.Fail();
            return;
        }

        var userEmail = _loggedInUserService.UserEmail;

        if (userEmail is null)
        {
            context.Fail();
            return;
        }

        var user = await _userSecurityRespository.GetUser(context.User);

        if (user is null)
        {
            context.Fail();
            return;
        }

        var claimList = requirement.ClaimValue.ToList();

        claimList.Add(SuperUserPermissions.All);

        var hasClaim = await _userSecurityRespository.HasClaim(user.Id, claimList);

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
