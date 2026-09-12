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
    public async Task<ActionResult<IEnumerable<Bracelet>>> GetAll()
    {
        var result = await BraceletRepository.SearchAsync();
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Bracelet>> GetById(int id)
    {
        var result = await BraceletRepository.FindByIdAsync(id);
        return result.ToActionResult();
    }
}