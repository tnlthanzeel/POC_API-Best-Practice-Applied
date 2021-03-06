using MediatR;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using POC.Application.Features.Users.Command.CreateUser;
using POC.Application.Features.Users.Command.DeleteUserCommand;
using POC.Application.Features.Users.Command.UpdateUser;
using POC.Application.Features.Users.Queries.GetUserDetail;
using POC.Application.Features.Users.Queries.GetUserList;
using POC.Application.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace POC.Api.Controllers;

[Route("api/users")]
public class UsersController : AppControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllUsers")]
    [ProducesResponseType(typeof(ResponseResult<IEnumerable<UserViewModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllUsers()
    {
        var responseResult = await _mediator.Send(new GetUsersListQuery());
        return responseResult.Success ? Ok(responseResult) : UnsuccessfullResponse(responseResult);
    }

    /// <summary>
    /// Get a user by user id
    /// </summary>
    /// <param name="id">The id of the user you want to get</param>
    /// <returns>A user object</returns>
    [HttpGet("{id}", Name = "GetUser")]
    [ProducesResponseType(typeof(ResponseResult<UserDetailViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]

    public async Task<ActionResult> GetUser(Guid id)
    {
        var viewModel = await _mediator.Send(new GetUserDetailQuery() { UserId = id });

        return viewModel.Success ? Ok(viewModel) : UnsuccessfullResponse(viewModel);
    }

    /// <summary>
    /// some xml comment
    /// </summary>
    /// <param name="createUserCommand"></param>
    /// <returns></returns>
    [HttpPost(Name = "AddUser")]
    [SwaggerOperation(
    Summary = "Creates a new Project",
    Description = "Creates a new Project description",
    OperationId = "Project.Create")
    //Tags = new[] { "ProjectEndpoints" })
        ]

    [ProducesResponseType(typeof(ResponseResult<CreateUserCommandResponse>), StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] CreateUserCommand createUserCommand)
    {
        var response = await _mediator.Send(createUserCommand);

        return response.Success ? CreatedAtRoute(nameof(GetUser), new { id = response.Data!.Id }, response) : UnsuccessfullResponse(response);

    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand updateUserCommand)
    {
        updateUserCommand.Id = id;
        var response = await _mediator.Send(updateUserCommand);

        return response.Success ? NoContent() : UnsuccessfullResponse(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleteUserCommand = new DeleteUserCommand() { Id = id };

        var response = await _mediator.Send(deleteUserCommand);

        return response.Success ? NoContent() : UnsuccessfullResponse(response);
    }
}
