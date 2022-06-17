using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Queries.GetUserDetail;

/// <summary>
/// A user detail mode
/// </summary>
public record UserDetailViewModel
{
    /// <summary>
    /// Id of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// First name of the user
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Last name of the user
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Address of the user
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// School of the user
    /// </summary>
    public string School { get; set; } = null!;

    /// <summary>
    /// Gender of the user
    /// </summary>
    public Gender Gender { get; set; }
}
