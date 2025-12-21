namespace HotelBooking.Web.Controllers.Base;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Result;

[ApiController]
public class BaseApiController : ControllerBase
{
    protected IActionResult FromResult(Result executionResult)
    {
        if (executionResult.IsSuccess)
        {
            return Ok(executionResult);
        }

        var status = executionResult.Status == 0
            ? (int)HttpStatusCode.BadRequest
            : executionResult.Status;

        return StatusCode(status, executionResult);
    }

    protected IActionResult FromResult<T>(Result<T> executionResult)
    {
        if (executionResult.IsSuccess)
        {
            return Ok(executionResult);
        }

        var status = executionResult.Status == 0
            ? (int)HttpStatusCode.BadRequest
            : executionResult.Status;

        return StatusCode(status, executionResult);
    }
}