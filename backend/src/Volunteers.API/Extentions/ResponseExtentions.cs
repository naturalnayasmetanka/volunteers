﻿using Microsoft.AspNetCore.Mvc;
using Volunteers.Domain.Shared.Enums;
using Volunteers.Domain.Shared.Models.CustomErrors;

namespace Volunteers.API.Extentions;

public static class ResponseExtentions
{
    public static ActionResult ToResponse(this Error error)
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
}