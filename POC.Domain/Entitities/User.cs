using static POC.Utility.BaseEnums;

namespace POC.Domain.Entitities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string School { get; set; } = null!;
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
