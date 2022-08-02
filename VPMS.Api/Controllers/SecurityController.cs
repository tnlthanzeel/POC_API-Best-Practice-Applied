using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPMS.Application.Contracts.Security;
using VPMS.Application.Security.Dtos;
using VPMS.Domain.AuthPolicies;
using VPMS.SharedKernel.Responses;

namespace VPMS.Api.Controllers;

[Route("api/security")]
public class SecurityController : AppControllerBase
{
    private readonly ISecurityService _securityService;

    public SecurityController(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    /// <summary>
    /// Authenticate a user by providing the username and password
    /// </summary>
    /// <param name="model"></param>
    /// <returns>A Respoonse result object conataining the authenticated user info</returns>
    [AllowAnonymous]
    [HttpPost("users/authenticate")]
    [ProducesResponseType(typeof(ResponseResult<AuthenticatedUserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Authenticate([FromBody] AuthenticateUserDto model, CancellationToken token)
    {
        var response = await _securityService.AuthenticateUser(model, token);

        return response.Success ? Ok(response) : UnsuccessfullResponse(response);
    }

    [HttpPost("users")]
    [Authorize(policy: UserAuthPolicy.Create)]
    [ProducesResponseType(typeof(ResponseResult<UserDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateUser([FromBody] UserCreateDto model, CancellationToken token)
    {
        var response = await _securityService.CreateUser(model, token);

        return response.Success ? CreatedAtRoute(nameof(GetUserById), new { id = response.Data!.Id }, response) : UnsuccessfullResponse(response);
    }

    [HttpGet("users/{id}", Name = "GetUserById")]
    [Authorize(policy: UserAuthPolicy.View)]
    [ProducesResponseType(typeof(ResponseResult<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserById([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _securityService.Getuser(id, token);

        return response.Success ? Ok(response) : UnsuccessfullResponse(response);
    }
}
