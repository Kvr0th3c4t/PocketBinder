using Microsoft.EntityFrameworkCore;
using PocketBinder.Data;
using PocketBinder.DTOs.Binder;
using PocketBinder.Exceptions;
using PocketBinder.Models;
using PocketBinder.Services.TcgApiServices;

namespace PocketBinder.Services.UserCollectionService
{
    public class UserCollectionService : IUserCollectionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserCollectionService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddCardToCollectionAsync(string CardId, int Quantity)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);

            if (await _context.UserCollections.AnyAsync(uc => uc.UserId == userId && uc.CardId == CardId))
            {
                throw new BadRequestException("Card already exists in collection. Use UpdateCardQuantityAsync to change quantity.");
            }
            else
            {
                await _context.UserCollections.AddAsync(new UserCollection
                {
                    UserId = userId,
                    CardId = CardId,
                    Quantity = Quantity,
                    AddedAt = DateTime.UtcNow

                });
            }
               
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CardSummaryDto>> GetUserCollectionAsync()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);

            return await _context.UserCollections
                 .Where(uc => uc.UserId == userId)
                 .Include(uc => uc.User)
                 .Join(_context.Cards,
                     uc => uc.CardId,
                     c => c.Id,
                     (uc, c) => new CardSummaryDto
                     {
                         Id = c.Id,
                         Name = c.Name,
                         SetId = c.SetId,
                         SetName = c.Set.Name,
                         Number = c.Number,
                         Rarity = c.Rarity,
                         SmallImageUrl = c.SmallImageUrl,
                         Quantity = uc.Quantity
                     })
                 .ToListAsync();
        }

        public async Task RemoveCardFromCollectionAsync(string cardId)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);

            if (!await _context.UserCollections.AnyAsync(uc => uc.UserId == userId && uc.CardId == cardId))
            {
                throw new NotFoundException("Card not found in collection. Or access denied.");
            }

            var entry = await _context.UserCollections
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CardId == cardId);
            
            _context.UserCollections.Remove(entry);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateCardQuantityAsync(string cardId, int quantity)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);

            var entry = await _context.UserCollections
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CardId == cardId);
            if (entry == null)
            {
                throw new BadRequestException("Card not found in collection. Use AddCardToCollectionAsync to add it first.");
            }

            entry.Quantity = quantity;
            await  _context.SaveChangesAsync();
           
        }
    }
}
