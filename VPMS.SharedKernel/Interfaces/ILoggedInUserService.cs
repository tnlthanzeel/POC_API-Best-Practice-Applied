namespace VPMS.SharedKernel.Interfaces;

public interface ILoggedInUserService
{
    public string? UserId { get; }

    public string? UserEmail { get; }
}
