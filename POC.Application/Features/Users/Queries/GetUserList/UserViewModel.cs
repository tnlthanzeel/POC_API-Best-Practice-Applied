using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Queries.GetUserList
{
    public class UserViewModel 
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string School { get; set; }
        public Gender Gender { get; set; }
    }
}
