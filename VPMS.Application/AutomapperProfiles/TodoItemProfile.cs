using AutoMapper;
using VPMS.Application.TodoItem.Dtos;

namespace VPMS.Application.AutomapperProfiles;

internal sealed class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<CreateTodoItemDto, Domain.Entities.TodoItemEntities.TodoItem>();
        CreateMap<UpdateTodoItemDto, Domain.Entities.TodoItemEntities.TodoItem>();
        CreateMap<Domain.Entities.TodoItemEntities.TodoItem, TodoItemDto>();
    }
}
