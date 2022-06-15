using AutoMapper;
using POC.Application.Features.Users.Command.CreateUser;
using POC.Application.Features.Users.Command.UpdateUser;
using POC.Application.Features.Users.Queries.GetUserDetail;
using POC.Application.Features.Users.Queries.GetUserList;

namespace POC.Application.AutoMapperProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<POC.Domain.Entitities.User, UserViewModel>().ReverseMap();
        CreateMap<POC.Domain.Entitities.User, UserDetailViewModel>();
        CreateMap<POC.Domain.Entitities.User, CreateUserCommandResponse>();
        CreateMap<UpdateUserCommand, POC.Domain.Entitities.User>();
    }
}
