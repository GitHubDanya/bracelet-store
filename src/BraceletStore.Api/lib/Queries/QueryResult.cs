using System;
using Microsoft.AspNetCore.Mvc;

namespace BraceletStore.Api.lib.Queries;

public record QueryResult<T>(T? Data, QueryStatus Status, Exception? Exception = null)
{
    public static QueryResult<T> Ok(T data) =>
        new(data, QueryStatus.Ok);
    public  static QueryResult<T> NotFound() =>
        new(default, QueryStatus.NotFound);
    public static QueryResult<T> Fail(Exception exception) =>
        new(default, QueryStatus.Failed, exception);
    public ActionResult<T> ToActionResult() =>
        Status switch
        {
            QueryStatus.Ok => new OkObjectResult(Data),
            QueryStatus.NotFound => new NotFoundResult(),
            _ => new ObjectResult(new { error = Exception?.Message ?? "An unexpected server error occurred." })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            }
        };
}

public enum QueryStatus
{
    Ok,
    NotFound,
    Failed
}