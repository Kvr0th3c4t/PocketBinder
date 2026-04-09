using PocketBinder.DTOs.Binder;
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

        // Métodos para obtener cartas con soporte para paginación, búsqueda y ordenamiento
        [Get("/cards")]
        Task<PaginatedResponseDto<CardSummaryDto>> SearchCardsAsync(
            [AliasAs("page")] int page,
            [AliasAs("pageSize")] int pageSize,
            [AliasAs("q")] string q = "",
            [AliasAs("orderBy")] string orderBy = "");

        // Método para obtener los sets de cartas con soporte para paginación, búsqueda y ordenamiento
        [Get("/sets")]
        Task<PaginatedResponseDto<SetDto>> SearchSetsAsync(
            [AliasAs("page")] int Page,
            [AliasAs("pageSize")] int PageSize,
            [AliasAs("q")] string q = "",
            [AliasAs("orderBy")] string orderBy = "");
    }
}
