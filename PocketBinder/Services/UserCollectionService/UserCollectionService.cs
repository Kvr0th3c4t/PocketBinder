using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PocketBinder.Data;
using PocketBinder.DTOs.Binder;
using PocketBinder.Models;
using PocketBinder.Services.TcgApiServices;

namespace PocketBinder.Services.UserCollectionService
{
    public class UserCollectionService : IUserCollectionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPokemonTcgApi _pokemonTcgApi;

        public UserCollectionService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IPokemonTcgApi pokemonTcgApi)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _pokemonTcgApi = pokemonTcgApi;
        }

        public async Task AddCardToCollectionAsync(string CardId, int Quantity)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);

            if (await _context.UserCollections.AnyAsync(uc => uc.UserId == userId && uc.CardId == CardId))
            {
                throw new InvalidOperationException("Card already exists in collection. Use UpdateCardQuantityAsync to change quantity.");
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
            var cardIds = await _context.UserCollections
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CardId)
                .ToListAsync();
            var query = string.Join(" OR ", cardIds.Select(id => $"id:{id}"));

            await _pokemonTcgApi.SearchCardsAsync(0,0, query);

            throw new NotImplementedException();
        }

        public async Task RemoveCardFromCollectionAsync(string cardId)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);

            var entry = await _context.UserCollections
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CardId == cardId);
            if (entry == null)
            {
                throw new InvalidOperationException("Card not found in collection.");
            }
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
                throw new InvalidOperationException("Card not found in collection. Use AddCardToCollectionAsync to add it first.");
            }

            entry.Quantity = quantity;
            await  _context.SaveChangesAsync();
           
        }
    }
}
