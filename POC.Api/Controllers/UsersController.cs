using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.Application.Features.Users.Command.CreateUser;
using POC.Application.Features.Users.Command.DeleteUserCommand;
using POC.Application.Features.Users.Command.UpdateUser;
using POC.Application.Features.Users.Queries.GetUserDetail;
using POC.Application.Features.Users.Queries.GetUserList;
using POC.Application.Responses;

namespace POC.Api.Controllers;

[Route("api/users")]
//[ResponseCache(CacheProfileName = "DefaultCache")]
public class UsersController : AppControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllUsers")]
    //[ResponseCache(Duration = 120)]
    [ProducesResponseType(typeof(ResponseResult<IEnumerable<UserViewModel>>), StatusCodes.Status200OK)]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 120)]
    //[HttpCacheValidation(MustRevalidate = true)]
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
    //[ResponseCache(Duration = 120)]
    [ProducesResponseType(typeof(ResponseResult<UserDetailViewModel>), StatusCodes.Status200OK)]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 120)]
    //[HttpCacheValidation(MustRevalidate = true)]
    public async Task<ActionResult> GetUser(Guid id)
    {
        var viewModel = await _mediator.Send(new GetUserDetailQuery() { UserId = id });

        return viewModel.Success ? Ok(viewModel) : UnsuccessfullResponse(viewModel);
    }

    [HttpPost(Name = "AddUser")]
    [ProducesResponseType(typeof(ResponseResult<CreateUserCommandResponse>), StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] CreateUserCommand createUserCommand)
    {
        var response = await _mediator.Send(createUserCommand);

        return response.Success ? CreatedAtRoute(nameof(GetUser), new { id = response.Data!.Id }, response) : UnsuccessfullResponse(response);

    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand updateUserCommand)
    {
        updateUserCommand.Id = id;
        var response = await _mediator.Send(updateUserCommand);

        return response.Success ? NoContent() : UnsuccessfullResponse(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleteUserCommand = new DeleteUserCommand() { Id = id };

        var response = await _mediator.Send(deleteUserCommand);

        return response.Success ? NoContent() : UnsuccessfullResponse(response);
    }
}
