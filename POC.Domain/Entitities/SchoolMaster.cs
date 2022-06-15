namespace POC.Domain.Entitities
{
    public class SchoolMaster
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int NumberOfEmployees { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
