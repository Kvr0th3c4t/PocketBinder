using Microsoft.EntityFrameworkCore;
using PocketBinder.Data;
using PocketBinder.DTOs.Album;
using PocketBinder.Models;

namespace PocketBinder.Services.AlbumService
{
    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AlbumService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddCardToAlbumAsync(int albumId, string cardId)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
            if (!await _context.Albums.AnyAsync(a => a.AlbumId == albumId && a.UserId == userId))
                throw new Exception("Album not found or access denied");
            else 
            {
                await _context.AlbumCards.AddAsync(new AlbumCard
                {
                    AlbumId = albumId,
                    CardId = cardId
                });
            }
               
            await _context.SaveChangesAsync();
        }

        public async Task CreateAlbumAsync(CreateAlbumDto dto)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
            await _context.Albums.AddAsync(new Album
            {
                AlbumName = dto.Name,
                AlbumType = dto.Type,
                SetId = dto.SetId,
                UserId = userId,
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlbumAsync(int albumId)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
            await _context.Albums
                .Where(a => a.AlbumId == albumId && a.UserId == userId)
                .ExecuteDeleteAsync();
        }

        public async Task<AlbumDetailDto> GetAlbumDetailAsync(int albumId)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
            return await _context.Albums.Where(a => a.AlbumId == albumId)
                .Select(a => new AlbumDetailDto
                {
                    Id = a.AlbumId,
                    Name = a.AlbumName,
                    Type = a.AlbumType.ToString(),
                    SetId = a.SetId,
                    SetName = a.SetName,
                    Cards = a.AlbumCards.Select(ac => new AlbumCardDto
                    {
                        Id = ac.CardId,
                        Name = ac.Card.Name,
                        Rarity = ac.Card.Rarity,
                        SmallImageUrl = ac.Card.SmallImageUrl,
                        Number = ac.Card.Number,
                        IsOwned = _context.UserCollections.Any(uc => uc.UserId == userId && uc.CardId == ac.CardId)
                    }).ToList()
                }).FirstOrDefaultAsync() ?? throw new Exception("Album not found");
        }

        public async Task<IEnumerable<AlbumSummaryDto>> GetUserAlbumsAsync()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);

            return await _context.Albums.Where(a => a.UserId == userId)
                .Select(a => new AlbumSummaryDto
                {
                    Id = a.AlbumId,
                    Name = a.AlbumName,
                    Type = a.AlbumType.ToString(),
                    SetId = a.SetId,
                    SetName = a.SetName
                }).ToListAsync();
        }

        public async Task RemoveCardFromAlbumAsync(int albumId, string cardId)
        {
            await _context.AlbumCards.Where(ac => ac.AlbumId == albumId && ac.CardId == cardId).ExecuteDeleteAsync();
        }
    }
}
