using System;
using System.Data;
using System.Threading.Tasks;
using BraceletStore.Api.lib.Queries;
using Npgsql;

namespace BraceletStore.Api.Database;

public static class DataProvider
{
    private static readonly string ConnectionString;
    
    static DataProvider()
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "bracelet_db";
        var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "dev_user";
        var dbPass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "dev_password";

        ConnectionString = new NpgsqlConnectionStringBuilder
        {
            Host = dbHost,
            Port = 5432,
            Database = "app_db",
            Username = dbUser,
            Password = dbPass
        }.ConnectionString;
    }
    
    public static async Task<QueryResult<T>> QuerySingleAsync<T>(Func<IDbConnection, Task<T?>> action)
    {
        try
        {
            using var db = new NpgsqlConnection(ConnectionString);
            var data = await action(db);

            return data is not null 
                ? QueryResult<T>.Ok(data) 
                : QueryResult<T>.NotFound();
        }
        catch (Exception ex)
        {
            return QueryResult<T>.Fail(ex);
        }
    }
}