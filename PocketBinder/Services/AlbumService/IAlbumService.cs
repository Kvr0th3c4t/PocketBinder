using PocketBinder.DTOs.Album;

namespace PocketBinder.Services.AlbumService
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumSummaryDto>> GetUserAlbumsAsync();
        Task<AlbumDetailDto> GetAlbumDetailAsync(int albumId);
        Task CreateAlbumAsync(CreateAlbumDto dto);
        Task DeleteAlbumAsync(int albumId);
        Task AddCardToAlbumAsync(int albumId, string cardId);
        Task RemoveCardFromAlbumAsync(int albumId, string cardId);
    }
}
