namespace BraceletStore.Api.Models.Bracelet;

public class Bracelet
{
    public int Id { get; set; }
    public LocalizedRecord Name { get; set; }
    public LocalizedRecord Description { get; set; }
    public List<LocalizedRecord> Materials { get; set; }
    public LocalizedRecord Color { get; set; }
}