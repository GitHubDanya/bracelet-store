using System;

namespace BraceletStore.Api.lib.Queries;

public record QueryResult<T>(T? Data, QueryStatus Status, Exception? Exception = null)
{
    public static QueryResult<T> Ok(T data) =>
        new(data, QueryStatus.Ok);
    public  static QueryResult<T> NotFound() =>
        new(default, QueryStatus.NotFound);
    public static QueryResult<T> Fail(Exception exception) =>
        new(default, QueryStatus.Failed, exception);
}

public enum QueryStatus
{
    Ok,
    NotFound,
    Failed
}