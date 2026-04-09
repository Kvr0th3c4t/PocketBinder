using PocketBinder.DTOs.Binder;
using PocketBinder.DTOs.Pagination;
using PocketBinder.DTOs.Query;

namespace PocketBinder.Services.TcgApiServices
{
    public interface IPokemonTcgService
    {
        Task<PaginatedResponseDto<CardSummaryDto>> SearchCardsAsync(CardQueryDto query);
        Task<PaginatedResponseDto<SetDto>> SearchSetsAsync(SetQueryDto query);
    }
}
