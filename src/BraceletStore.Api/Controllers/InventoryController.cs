using BraceletStore.Api.Models;
using BraceletStore.Api.Models.Bracelet;
using Microsoft.AspNetCore.Mvc;

namespace BraceletStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private static readonly List<Bracelet> Bracelets = new()
    {
        new Bracelet
        {
            Id = 1,
            Available = true,
            ThumbnailUrls = new List<string>() { "Lorem" },
            Name = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
            Description = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
            Materials = new List<LocalizedRecord>() { new LocalizedRecord("Lorem", "Lorem", "Lorem"), },
            Color = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
        },
        new Bracelet
        {
            Id = 2,
            Available = true,
            ThumbnailUrls = new List<string>() { "Lorem" },
            Name = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
            Description = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
            Materials = new List<LocalizedRecord>() { new LocalizedRecord("Lorem", "Lorem", "Lorem"), },
            Color = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
        },
        new Bracelet
        {
            Id = 3,
            Available = true,
            ThumbnailUrls = new List<string>() { "Lorem" },
            Name = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
            Description = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
            Materials = new List<LocalizedRecord>() { new LocalizedRecord("Lorem", "Lorem", "Lorem"), },
            Color = new LocalizedRecord("Lorem", "Lorem", "Lorem"),
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Bracelet>> GetAll()
    {
        return Ok(Bracelets);
    }

    [HttpGet("{id}")]
    public ActionResult<Bracelet> GetById(int id)
    {
        Bracelet? bracelet = Bracelets.FirstOrDefault(b => b.Id == id);
        return bracelet is not null ? Ok(bracelet) : NotFound();
    }
}