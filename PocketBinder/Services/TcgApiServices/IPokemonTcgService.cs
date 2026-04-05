using PocketBinder.DTOs.API;
using PocketBinder.DTOs.Binder;
using PocketBinder.DTOs.Query;

namespace PocketBinder.Services.TcgApiServices
{
    public interface IPokemonTcgService
    {
        Task<PaginatedResponseDto<CardSummaryDto>> GetCardsAsync(CardQueryDto query);
        Task<PaginatedResponseDto<CardSetDto>> GetSetsAsync(SetQueryDto query);
    }
}
