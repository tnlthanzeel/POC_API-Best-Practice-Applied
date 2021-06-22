using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Domain.Entitities
{
    public class SchoolMaster
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
