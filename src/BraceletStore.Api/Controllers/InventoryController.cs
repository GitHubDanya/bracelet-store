using BraceletStore.Api.Database.Repositories;
using BraceletStore.Api.lib.Queries;
using BraceletStore.Api.Models.Bracelet;
using Microsoft.AspNetCore.Mvc;

namespace BraceletStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bracelet>>> GetAll([FromQuery] BraceletFilter filter)
    {
        var result = await BraceletRepository.SearchAsync(filter);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Bracelet>> GetById(int id)
    {
        var result = await BraceletRepository.FindByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("materials")]
    public async Task<ActionResult<List<string>>> GetMaterials([FromQuery] BraceletFilter filter)
    {
        var result = await BraceletRepository.FetchMaterials(filter);
        return result.ToActionResult();
    }
}