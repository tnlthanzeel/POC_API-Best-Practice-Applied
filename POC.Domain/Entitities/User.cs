using System;
using static POC.Utility.BaseEnums;

namespace POC.Domain.Entitities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string School { get; set; }
        public Gender Gender { get; set; }


        public User Create(string firstName, string lastName, string address, string school, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            School = school;
            Gender = gender;

            return this;
        }
    }
}
