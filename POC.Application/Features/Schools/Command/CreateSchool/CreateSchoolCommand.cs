using MediatR;

namespace POC.Application.Features.Schools.Command.CreateSchool
{
    public class CreateSchoolCommand : IRequest<CreateSchoolCommandResponse>
    {
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int NumberOfEmployees { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
