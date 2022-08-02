using System.Security.Claims;
using VPMS.SharedKernel.Interfaces;

namespace VPMS.Api.Services;

public sealed class LoggedInUserService : ILoggedInUserService
{
    public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        UserEmail = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Email)?.Value;
    }

    public string? UserId { get; }

    public string? UserEmail { get; }
}
