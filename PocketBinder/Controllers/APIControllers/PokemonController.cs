using Microsoft.AspNetCore.Mvc;
using PocketBinder.DTOs.Query;
using PocketBinder.Services.TcgApiServices;


namespace PocketBinder.Controllers.APIControllers;

[ApiController]
[Route("api/[controller]")]

public class PokemonController : ControllerBase
{
    // Inyectamos el servicio de Pokémon TCG para manejar las solicitudes relacionadas con cartas y sets
    private readonly IPokemonTcgService _pokemonTcgService;
    public PokemonController(IPokemonTcgService pokemonTcgService)
    {
        _pokemonTcgService = pokemonTcgService;
    }

    // Endpoint para buscar cartas con filtros y paginación
    [HttpGet("cards")]
    public async Task<IActionResult> GetCards([FromQuery] CardQueryDto query)
    {
        var result = await _pokemonTcgService.GetCardsAsync(query);
        return Ok(result);
    }

    // Endpoint para buscar sets con filtros y paginación
    [HttpGet("sets")]
    public async Task<ActionResult> GetSets([FromQuery] SetQueryDto query)
    {
        var result = await _pokemonTcgService.GetSetsAsync(query);
        return Ok(result);
    }

}
