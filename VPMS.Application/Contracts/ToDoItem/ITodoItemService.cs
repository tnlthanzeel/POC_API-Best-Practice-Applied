using VPMS.Application.TodoItem;
using VPMS.Application.TodoItem.Dtos;
using VPMS.SharedKernel.Models;

namespace VPMS.Application.Contracts.ToDoItem;

public interface ITodoItemService
{
    Task<ResponseResult<TodoItemDto>> Add(CreateTodoItemDto model, CancellationToken token);

    Task<ResponseResult<TodoItemDto>> GetById(Guid id, CancellationToken token);

    Task<ResponseResult<IReadOnlyList<TodoItemDto>>> GetList(Paginator paginator, ToDoItemFilter filter, CancellationToken token);

    Task<ResponseResult> Update(Guid id, UpdateTodoItemDto model, CancellationToken token);

    Task<ResponseResult> Delete(Guid id, CancellationToken token);
}
