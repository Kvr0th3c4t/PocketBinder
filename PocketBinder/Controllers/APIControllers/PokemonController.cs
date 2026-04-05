using Microsoft.AspNetCore.Mvc;
using PocketBinder.DTOs.Query;
using PocketBinder.Services.TcgApiServices;


namespace PocketBinder.Controllers.APIControllers;

[ApiController]
[Route("api/[controller]")]

public class PokemonController : ControllerBase
{

    private readonly IPokemonTcgService _pokemonTcgService;
    public PokemonController(IPokemonTcgService pokemonTcgService)
    {
        _pokemonTcgService = pokemonTcgService;
    }

    [HttpGet("cards")]
    public async Task<IActionResult> GetCards([FromQuery] CardQueryDto query)
    {
        var result = await _pokemonTcgService.GetCardsAsync(query);
        return Ok(result);
    }

    [HttpGet("sets")]
    public async Task<ActionResult> GetSets([FromQuery] SetQueryDto query)
    {
        var result = await _pokemonTcgService.GetSetsAsync(query);
        return Ok(result);
    }

}
