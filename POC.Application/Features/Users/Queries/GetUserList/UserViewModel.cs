using System;
using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Queries.GetUserList
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string School { get; set; } = null!;
        public Gender Gender { get; set; }
    }
}
