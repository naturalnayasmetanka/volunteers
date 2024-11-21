using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.API.Extentions;

public static class ResponseExtentions
{
    public static ActionResult ToErrorResponse(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        return new ObjectResult(error)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToErrorResponse(this List<Error> errors)
    {
        foreach (var error in errors)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        return new ObjectResult(errors)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }

    public static ActionResult FromFluientToErrorResponse(this List<ValidationFailure> errors)
    {
        List<Error> _errors = [];

        foreach (var error in errors)
        {
            _errors.Add(Errors.General.ValueIsInvalid(error.PropertyName));
        }

        return new ObjectResult(_errors)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}