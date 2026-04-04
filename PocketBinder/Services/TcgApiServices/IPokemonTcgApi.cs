using PocketBinder.DTOs.API;
using PocketBinder.DTOs.Binder;
using PocketBinder.DTOs.Query;
using Refit;

namespace PocketBinder.Services.TcgApiServices
{
    public interface IPokemonTcgApi
    {
        [Get("/cards")]
        Task <PaginatedResponseDto<CardSummaryDto>> GetCardsAsync([Query] CardQueryDto query);

        [Get("/sets")]
        Task<PaginatedResponseDto<CardSetDto>> GetSetsAsync([Query] SetQueryDto query);
    }
}
