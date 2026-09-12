using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using BraceletStore.Api.lib.Queries;
using BraceletStore.Api.Models;
using Dapper;
using Npgsql;

namespace BraceletStore.Api.Database;

public static class DataProvider
{
    private static readonly NpgsqlDataSource DataSource;
    
    static DataProvider()
    {
        SqlMapper.AddTypeHandler(new JsonTypeHandler<LocalizedRecord>());
        SqlMapper.AddTypeHandler(new JsonTypeHandler<List<LocalizedRecord>>());
        
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "127.0.0.1";
        var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "dev_user";
        var dbPass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "dev_password";

        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = dbHost,
            Port = 5432,
            Database = "bracelet_db",
            Username = dbUser,
            Password = dbPass,
            IncludeErrorDetail = true
        }.ConnectionString;

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        
        dataSourceBuilder.ConfigureJsonOptions(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        dataSourceBuilder.EnableDynamicJson();

        DataSource = dataSourceBuilder.Build();
    }
    
    public static async Task<QueryResult<T>> QuerySingleAsync<T>(Func<IDbConnection, Task<T?>> action)
    {
        try
        {
            await using var db = await DataSource.OpenConnectionAsync();
            var data = await action(db);

            return data is not null 
                ? QueryResult<T>.Ok(data) 
                : QueryResult<T>.NotFound();
        }
        catch (PostgresException pgEx)
        {
            var detailedMessage = $"""
                                   Postgres Error [{pgEx.SqlState}]: {pgEx.MessageText}
                                   Detail: {pgEx.Detail ?? "None"}
                                   Hint: {pgEx.Hint ?? "None"}
                                   Constraint: {pgEx.ConstraintName ?? "None"}
                                   Table: {pgEx.TableName ?? "None"}
                                   """;

            Console.Error.WriteLine($"[Database Error]\n{detailedMessage}");

            return QueryResult<T>.Fail(new InvalidOperationException(detailedMessage, pgEx));
        }
        catch (Exception ex)
        {
            return QueryResult<T>.Fail(ex);
        }
    }

    public static async Task<QueryResult<T>> QueryManyAsync<T>(Func<IDbConnection, Task<T>> action)
    {
        try
        {
            await using var db = await DataSource.OpenConnectionAsync();
            var data = await action(db);

            return QueryResult<T>.Ok(data);
        }
        catch (Exception ex)
        {
            return QueryResult<T>.Fail(ex);
        }
    }
}

public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public override void SetValue(IDbDataParameter parameter, T? value)
    {
        parameter.Value = value is null ? DBNull.Value : JsonSerializer.Serialize(value, _options);
    }

    public override T? Parse(object value)
    {
        if (value is string json)
        {
            return JsonSerializer.Deserialize<T>(json, _options);
        }
        return default;
    }
}