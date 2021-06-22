using Marvin.Cache.Headers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC.Application.Features.Users.Command.CreateUser;
using POC.Application.Features.Users.Command.DeleteUserCommand;
using POC.Application.Features.Users.Command.UpdateUser;
using POC.Application.Features.Users.Queries.GetUserDetail;
using POC.Application.Features.Users.Queries.GetUserList;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[ResponseCache(CacheProfileName = "DefaultCache")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllUsers")]
        //[ResponseCache(Duration = 120)]
        [ProducesResponseType(typeof(SuccessResponse<IEnumerable<UserViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 120)]
        //[HttpCacheValidation(MustRevalidate = true)]
        public async Task<ActionResult<SuccessResponse<IEnumerable<UserViewModel>>>> GetAllUsers()
        {
            var viewModel = await _mediator.Send(new GetUsersListQuery());
            return Ok(viewModel);
        }


        [HttpGet("{id}", Name = "GetUser")]
        //[ResponseCache(Duration = 120)]
        [ProducesResponseType(typeof(SuccessResponse<UserDetailViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 120)]
        //[HttpCacheValidation(MustRevalidate = true)]
        public async Task<ActionResult<SuccessResponse<UserDetailViewModel>>> GetUser(Guid id)
        {
            var viewModel = await _mediator.Send(new GetUserDetailQuery() { UserId = id });
            return Ok(viewModel);
        }

        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(typeof(SuccessResponse<CreateUserCommandResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<SuccessResponse<CreateUserCommandResponse>>> Create([FromBody] CreateUserCommand createUserCommand)
        {
            var response = await _mediator.Send(createUserCommand);

            return CreatedAtRoute(nameof(GetUser), new { id = response.Data.Id }, response);


        }

        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<CreateUserCommandResponse>>> UpdateUser(Guid id, [FromBody] UpdateUserCommand updateUserCommand)
        {
            updateUserCommand.Id = id;
            await _mediator.Send(updateUserCommand);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteUserCommand = new DeleteUserCommand() { Id = id };
            await _mediator.Send(deleteUserCommand);
            return NoContent();
        }
    }
}
