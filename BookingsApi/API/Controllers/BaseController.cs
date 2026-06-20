using BookingsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingsApi.Controllers;

public class BaseController : ControllerBase
{
    protected ActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess)
        {
            return Ok();
        }

        return ToErrorResult(result);
    }

    protected ActionResult ToNoContentResult(Result result)
    {
        if (result.IsSuccess)
        {
            return NoContent();
        }

        return ToErrorResult(result);
    }

    protected ActionResult<T> ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return ToErrorResult(result);
    }

    protected ActionResult<T> ToCreatedResult<T>(
        Result<T> result,
        string actionName,
        Func<T, object> routeValues)
    {
        if (result.IsSuccess)
        {
            return CreatedAtAction(actionName, routeValues(result.Value!), result.Value);
        }

        return ToErrorResult(result);
    }

    private ActionResult ToErrorResult(Result result) => result.Type switch
    {
        ResultType.NotFound => NotFound(ToProblemDetails(result)),
        ResultType.ValidationError => BadRequest(ToProblemDetails(result)),
        ResultType.Unauthorized => Unauthorized(ToProblemDetails(result)),
        ResultType.Conflict => Conflict(ToProblemDetails(result)),
        _ => BadRequest(ToProblemDetails(result))
    };

    private static ProblemDetails ToProblemDetails(Result result) => new()
    {
        Title = result.Type.ToString(),
        Detail = result.ErrorMessage,
        Status = MapToStatusCode(result.Type)
    };

    private static int MapToStatusCode(ResultType type) => type switch
    {
        ResultType.NotFound => StatusCodes.Status404NotFound,
        ResultType.ValidationError => StatusCodes.Status400BadRequest,
        ResultType.Unauthorized => StatusCodes.Status401Unauthorized,
        ResultType.Conflict => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };
}
