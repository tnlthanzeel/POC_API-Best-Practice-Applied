﻿using System;
using System.Collections.Generic;
using System.Text;
using static POC.Utility.BaseEnums;

namespace POC.Application.Features.Users.Queries.GetUserDetail
{
   public  class UserDetailViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string School { get; set; } = null!;
        public Gender Gender { get; set; }
    }
}
