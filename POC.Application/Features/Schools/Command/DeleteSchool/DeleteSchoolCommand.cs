using MediatR;

namespace POC.Application.Features.Schools.Command.DeleteSchool
{
    public class DeleteSchoolCommand : IRequest
    {
        public string Id { get; set; } = null!;
    }
}
