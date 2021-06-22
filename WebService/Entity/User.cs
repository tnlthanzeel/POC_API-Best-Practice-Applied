using WebService.Common.Enum;

namespace WebService.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string School { get; set; }
        public Gender Gender { get; set; }
    }
}
