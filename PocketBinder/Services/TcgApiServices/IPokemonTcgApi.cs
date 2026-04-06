using PocketBinder.DTOs.API;
using PocketBinder.DTOs.Binder;
using Refit;

namespace PocketBinder.Services.TcgApiServices
{
    // Interfaz para consumir la API de Pokémon TCG utilizando Refit
    public interface IPokemonTcgApi
    {
        // Métodos para obtener cartas y sets con soporte para paginación, búsqueda y ordenamiento
        [Get("/cards")]
        Task<PaginatedResponseDto<CardSummaryDto>> GetCardsAsync(
            [AliasAs("page")] int page,
            [AliasAs("pageSize")] int pageSize,
            [AliasAs("q")] string q = "",
            [AliasAs("orderBy")] string orderBy = "");

        // Método para obtener los sets de cartas con soporte para paginación, búsqueda y ordenamiento
        [Get("/sets")]
        Task<PaginatedResponseDto<CardSetDto>> GetSetsAsync(
            [AliasAs("page")] int Page,
            [AliasAs("pageSize")] int PageSize,
            [AliasAs("q")] string q = "",
            [AliasAs("orderBy")] string orderBy = "");
    }
}
