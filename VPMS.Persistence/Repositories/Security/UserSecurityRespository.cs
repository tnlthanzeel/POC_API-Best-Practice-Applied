using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VPMS.Application.PersistanceInterfaces;
using VPMS.Application.Security.Dtos;
using VPMS.Domain.Entities.IdentityUserEntities;
using VPMS.SharedKernel.Responses;

namespace VPMS.Persistence.Repositories.Security;

public class UserSecurityRespository : IUserSecurityRespository
{
    private readonly VPMSDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<UserRole> _roleManager;

    public UserSecurityRespository(VPMSDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<UserRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ResponseResult<UserDto>> CreateUser(string userName, string password, string email, CancellationToken token)
    {
        ApplicationUser newUser = new()
        {
            UserName = userName,
            Email = email,
        };

        var identityResult = await _userManager.CreateAsync(newUser, password);

        if (identityResult.Succeeded is false)
        {
            var validationResult = identityResult.Errors.Select(s => new ValidationFailure()
            {
                PropertyName = s.Code,
                ErrorMessage = s.Description
            }).ToList();

            return new ResponseResult<UserDto>(validationResult);
        }

        UserDto user = new(newUser.Id, newUser.Email, newUser.UserName);

        return new ResponseResult<UserDto>(user);

    }

    public async Task<ApplicationUser?> FindByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user;
    }

    public async Task<IReadOnlyList<UserRole>> GetAllRoles(CancellationToken token)
    {
        var allRoles = await _roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken: token);

        return allRoles;
    }

    public async Task<ApplicationUser?> GetUser(ClaimsPrincipal user)
    {
        var applicationUser = await _userManager.GetUserAsync(user);

        return applicationUser;
    }

    public async Task<ApplicationUser?> GetUser(Guid id)
    {
        var applicationUser = await _userManager.FindByIdAsync(id.ToString());

        return applicationUser;
    }

    public async Task<IReadOnlyList<UserClaimsDto>> GetUserClaims(Guid userId, CancellationToken token)
    {
        var userClaims = await _dbContext.UserClaims
                                .AsNoTracking()
                                .Where(w => w.UserId == userId)
                                .Select(s => new UserClaimsDto()
                                {
                                    ClaimId = s.Id,
                                    UserId = s.UserId,
                                    ClaimType = s.ClaimType,
                                    ClaimValue = s.ClaimValue
                                })
                                .ToListAsync(cancellationToken: token);

        return userClaims;
    }

    public async Task<IList<string>> GetUserRoles(Guid userId, CancellationToken token)
    {

        var user = await _dbContext.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(w => w.Id == userId, cancellationToken: token);

        if (user == null) return new List<string>();

        var userRoleNames = await _userManager.GetRolesAsync(user);

        return userRoleNames;
    }

    public async Task<bool> HasClaim(Guid userId, IEnumerable<string> claimValue, CancellationToken token)
    {

        var hasClaim = await _dbContext.UserClaims
                                       .AsNoTracking()
                                       .AnyAsync(w => w.UserId == userId && claimValue.Contains(w.ClaimValue), cancellationToken: token);

        return hasClaim;
    }
}
