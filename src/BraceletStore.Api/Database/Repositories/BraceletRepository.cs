using BraceletStore.Api.lib.Queries;
using BraceletStore.Api.Models.Bracelet;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;

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

    public static async Task<QueryResult<IEnumerable<Bracelet>>> SearchAsync(BraceletFilter? filter = null)
    {
        filter ??= new BraceletFilter();
        var builder = new SqlBuilder();

        var selector = builder.AddTemplate("""
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
                                           /**where**/
                                           ORDER BY id DESC
                                           LIMIT @Limit OFFSET @Offset
                                           """,
            new
            {
                Limit = filter.PageSize,
                Offset = (filter.Page - 1) * filter.PageSize
            });

        if (filter.AvailableOnly == true)
            builder.Where("available = true");

        if (filter.MinPrice.HasValue)
            builder.Where("price >= @MinPrice", new { MinPrice = filter.MinPrice.Value });

        if (filter.MaxPrice.HasValue)
            builder.Where("price <= @MaxPrice", new { MaxPrice = filter.MaxPrice.Value });

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            builder.Where("(name->>@Lang ILIKE @Term OR description->>@Lang ILIKE @Term)",
                new { Term = $"%{filter.SearchTerm}%", Lang = filter.Lang });
        }

        if (!string.IsNullOrWhiteSpace(filter.Material))
        {
            builder.Where("materials @> jsonb_build_array(jsonb_build_object(@Lang, @Material))",
                new { Lang = filter.Lang, Material = filter.Material });
        }

        return await DataProvider.QueryManyAsync(db =>
            db.QueryAsync<Bracelet>(selector.RawSql, selector.Parameters));
    }

    public static async Task<QueryResult<List<string>>> FetchMaterials(BraceletFilter? filter = null)
    {
        filter ??= new BraceletFilter();
        
        string sql = """
                     SELECT DISTINCT item ->> @Lang AS material
                     FROM bracelets,
                          jsonb_array_elements(materials) AS item
                     WHERE available = true 
                       AND item ->> @Lang IS NOT NULL
                     ORDER BY material;
                     """;

        return await DataProvider.QuerySingleAsync(async db =>
        {
            var result = await db.QueryAsync<string>(sql, new { Lang = filter.Lang });
            Console.WriteLine(result.FirstOrDefault());
            return result.ToList();
        });
    }

}

public record BraceletFilter(
    string? SearchTerm = null,
    string? Material = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    bool? AvailableOnly = null,
    string Lang = "en",
    int Page = 1,
    int PageSize = 20
    );