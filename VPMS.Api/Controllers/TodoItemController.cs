using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPMS.Application.Contracts.ToDoItem;
using VPMS.Application.TodoItem;
using VPMS.Application.TodoItem.Dtos;
using VPMS.Domain.AuthPolicies;
using VPMS.SharedKernel.Models;
using VPMS.SharedKernel.Responses;

namespace VPMS.Api.Controllers;


[Route("api/todo-items")]
public class TodoItemController : AppControllerBase
{
    private readonly ITodoItemService _todoItemService;

    public TodoItemController(ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }

    /// <summary>
    /// Get a Todo item by Id
    /// </summary>
    /// <param name="id">The id of the TodoItem to get</param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetTodoItem")]
    [ProducesResponseType(typeof(ResponseResult<TodoItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetTodoItem([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _todoItemService.GetById(id, token);

        return response.Success ? Ok(response) : UnsuccessfullResponse(response);
    }

    /// <summary>
    /// Create a new TodoItem
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(policy: TodoItemAuthPolicy.Create)]
    [ProducesResponseType(typeof(ResponseResult<TodoItemDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> AddTodoItem([FromBody] CreateTodoItemDto model, CancellationToken token)
    {
        var response = await _todoItemService.Add(model, token);

        return response.Success ? CreatedAtRoute(nameof(GetTodoItem), new { id = response.Data!.Id }, response) : UnsuccessfullResponse(response);
    }

    /// <summary>
    /// Updates a TodoItem
    /// </summary>
    /// <param name="id"> id of the TodoItem to be updated</param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateTodoItem([FromRoute] Guid id, [FromBody] UpdateTodoItemDto model, CancellationToken token)
    {
        var response = await _todoItemService.Update(id, model, token);

        return response.Success ? NoContent() : UnsuccessfullResponse(response);
    }

    /// <summary>
    /// Delete a TodoItem
    /// </summary>
    /// <param name="id">id of the TodoItem to delete</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTodoItem([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _todoItemService.Delete(id, token);

        return response.Success ? NoContent() : UnsuccessfullResponse(response);
    }

    /// <summary>
    /// Get a list of paginated TodoItems
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(policy: TodoItemAuthPolicy.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<TodoItemDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTodoItemList([FromQuery] Paginator paginator, [FromQuery] ToDoItemFilter filter, CancellationToken token)
    {
        var response = await _todoItemService.GetList(paginator, filter, token);

        return response.Success ? Ok(response) : UnsuccessfullResponse(response);
    }
}
