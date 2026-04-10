using PocketBinder.DTOs.Pagination;
using PocketBinder.DTOs.Sync.SyncCard;
using PocketBinder.DTOs.Sync.SyncSet;
using Refit;

namespace PocketBinder.Services.TcgApiServices
{
    // Interfaz para consumir la API de Pokémon TCG utilizando Refit
    public interface IPokemonTcgApi
    {
        // Para sincronización
        // Métodos para obtener sets sin soporte para paginación, búsqueda ni ordenamiento
        [Get("/sets")]
        Task<PaginatedResponseDto<SyncSetDto>> GetSetsPageAsync(
            [AliasAs("page")] int page,
            [AliasAs("pageSize")] int pageSize);

        // Métodos para obtener cartas sin soporte para paginación, búsqueda ni ordenamiento
        [Get("/cards")]
        Task<PaginatedResponseDto<SyncCardDto>> GetCardsPageAsync(
            [AliasAs("page")] int page,
            [AliasAs("pageSize")] int pageSize,
            [AliasAs("q")] string q);
    }
}
