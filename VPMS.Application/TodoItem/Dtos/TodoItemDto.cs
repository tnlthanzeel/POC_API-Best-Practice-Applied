namespace VPMS.Application.TodoItem.Dtos;

public sealed record TodoItemDto(Guid Id, string Title, string Description, bool IsDone, DateTimeOffset CompletedOn);


