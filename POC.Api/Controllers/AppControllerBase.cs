using Microsoft.AspNetCore.Mvc;
using POC.Application.Responses;
using System.Net;

namespace POC.Api.Controllers;

[ApiController]
public abstract class AppControllerBase : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    protected ObjectResult UnsuccessfullResponse<T>(ResponseResult<T> responseResult)
    {
        return UnsuccessfullResponseHandler(responseResult);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected ObjectResult UnsuccessfullResponse(ResponseResult responseResult)
    {
        return UnsuccessfullResponseHandler(responseResult);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    private ObjectResult UnsuccessfullResponseHandler<T>(ResponseResult<T> responseResult)
    {
        ErrorResponse errorResponse = new()
        {
            Errors = responseResult.Errors
        };

        if (responseResult.HttpStatusCode == HttpStatusCode.BadRequest)
            return BadRequest(errorResponse);

        else if (responseResult.HttpStatusCode == HttpStatusCode.NotFound)
            return NotFound(errorResponse);

        else if (responseResult.HttpStatusCode == HttpStatusCode.Unauthorized)
            return Unauthorized(errorResponse);

        else
            return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, errorResponse);
    }
}
