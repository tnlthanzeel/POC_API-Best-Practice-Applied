﻿using MediatR;
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 120)]
    //[HttpCacheValidation(MustRevalidate = true)]
    public async Task<ActionResult<ResponseResult<IEnumerable<UserViewModel>>>> GetAllUsers()
    {
        var responseResult = await _mediator.Send(new GetUsersListQuery());
        return responseResult.Success ? Ok(responseResult) : UnsuccessfullResponse(responseResult);
    }


    [HttpGet("{id}", Name = "GetUser")]
    //[ResponseCache(Duration = 120)]
    [ProducesResponseType(typeof(ResponseResult<UserDetailViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseResult<UserDetailViewModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 120)]
    //[HttpCacheValidation(MustRevalidate = true)]
    public async Task<ActionResult<ResponseResult<UserDetailViewModel>>> GetUser(Guid id)
    {
        var viewModel = await _mediator.Send(new GetUserDetailQuery() { UserId = id });

        return viewModel.Success ? Ok(viewModel) : UnsuccessfullResponse(viewModel);
    }

    [HttpPost(Name = "AddUser")]
    [ProducesResponseType(typeof(ResponseResult<CreateUserCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseResult<CreateUserCommandResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<ResponseResult<CreateUserCommandResponse>>> Create([FromBody] CreateUserCommand createUserCommand)
    {
        var response = await _mediator.Send(createUserCommand);

        return response.Success ? CreatedAtRoute(nameof(GetUser), new { id = response.Data!.Id }, response) : UnsuccessfullResponse(response);

    }

    [HttpPut("{id}", Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseResult<Unit>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseResult<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseResult<CreateUserCommandResponse>>> UpdateUser(Guid id, [FromBody] UpdateUserCommand updateUserCommand)
    {
        updateUserCommand.Id = id;
        var response = await _mediator.Send(updateUserCommand);

        return response.Success ? NoContent() : UnsuccessfullResponse(response);
    }

    [HttpDelete("{id}", Name = "DeleteUser")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleteUserCommand = new DeleteUserCommand() { Id = id };

        var response = await _mediator.Send(deleteUserCommand);

        return response.Success ? NoContent() : UnsuccessfullResponse(response);
    }
}
