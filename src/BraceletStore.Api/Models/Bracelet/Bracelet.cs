namespace BraceletStore.Api.Models.Bracelet;

public class Bracelet
{
    public required int Id { get; set; }
    public required bool Available { get; set; }
    public required decimal Price { get; set; }
    public required string[] ThumbnailUrls { get; set; }
    public required LocalizedRecord Name { get; set; }
    public required LocalizedRecord Description { get; set; }
    public required List<LocalizedRecord> Materials { get; set; }
    public required LocalizedRecord Color { get; set; }
}