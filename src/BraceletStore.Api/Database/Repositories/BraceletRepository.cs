using System.Data;
using System.Threading.Tasks;
using BraceletStore.Api.lib.Queries;
using BraceletStore.Api.Models.Bracelet;
using Microsoft.AspNetCore.Components;
using Dapper;
using Npgsql;

namespace BraceletStore.Api.Database.Repositories;

public static class BraceletRepository
{
    public static async Task<QueryResult<Bracelet>> FindByIdAsync(int id)
    {
        const string sql = """
                           SELECT 
                               id AS Id, 
                               available AS Available, 
                               price AS Price, 
                               thumbnail_urls AS ThumbnailUrls, 
                               name AS Name, 
                               description AS Description, 
                               materials AS Materials, 
                               color AS Color
                           FROM bracelets
                           WHERE id = @Id;
                           """;
        return await DataProvider.QuerySingleAsync(db =>
            db.QueryFirstOrDefaultAsync<Bracelet>(sql, new { Id = id }));
    }
}