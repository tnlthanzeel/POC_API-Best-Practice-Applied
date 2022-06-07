using Microsoft.AspNetCore.Mvc;
using POC.Application.Responses;
using System.Net;
using System.Threading.Tasks;

namespace POC.Api.Controllers;

[ApiController]
public abstract class AppControllerBase : ControllerBase
{
    public ActionResult<ResponseResult<T>> UnsuccessfullResponse<T>(ResponseResult<T> responseResult)
    {
        if (responseResult.HttpStatusCode == HttpStatusCode.BadRequest)
            return BadRequest(responseResult);

        else if (responseResult.HttpStatusCode == HttpStatusCode.NotFound)
            return NotFound(responseResult);

        else
            return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, responseResult);

    }
}
