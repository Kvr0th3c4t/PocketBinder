
using Microsoft.EntityFrameworkCore;
using PocketBinder.Data;
using PocketBinder.DTOs.Binder;
using PocketBinder.DTOs.Sync.SyncCard;
using PocketBinder.DTOs.Sync.SyncSet;
using PocketBinder.Models;
using PocketBinder.Services.TcgApiServices;

namespace PocketBinder.Services.SyncService
{
    public class SyncService : ISyncService
    {
        private readonly IPokemonTcgApi _pokemonTcgApi;
        private readonly ApplicationDbContext _context;

        public SyncService(IPokemonTcgApi pokemonTcgApi, ApplicationDbContext context)
        {
            _pokemonTcgApi = pokemonTcgApi;
            _context = context;
        }
       
        public async Task SyncAsync()
        {
            int page = 1;
            int pageSize = 250;

            while (true)
            {
                var sets = await _pokemonTcgApi.GetSetsPageAsync(page, pageSize);

                foreach (SyncSetDto set in sets.Data)
                {
                    if (!await _context.Sets.AnyAsync(s => s.Id == set.Id))
                    {
                        _context.Sets.Add(new Models.Set
                        {
                            Id = set.Id,
                            Name = set.Name,
                            Series = set.Series,
                            ReleaseDate = set.ReleaseDate,
                            PrintedTotal = set.PrintedTotal,
                            Total = set.Total,
                            SymbolUrl = set.Images.Symbol,
                            LogoUrl = set.Images.Logo
                        });
                    }
                    else
                    {
                        var existingSet = await _context.Sets.FirstOrDefaultAsync(s => s.Id == set.Id);
                        existingSet.Name = set.Name;
                        existingSet.Series = set.Series;
                        existingSet.ReleaseDate = set.ReleaseDate;
                        existingSet.PrintedTotal = set.PrintedTotal;
                        existingSet.Total = set.Total;
                        existingSet.SymbolUrl = set.Images.Symbol;
                        existingSet.LogoUrl = set.Images.Logo;
                    }
                }

                await _context.SaveChangesAsync();
                if (page * pageSize >= sets.TotalCount) break;
                page++;
            }

            var setList = await _context.Sets.ToListAsync();
           
            foreach (var set in setList)
            {
                int cardPage = 1;
                while (true)
                {
                    var cards = await _pokemonTcgApi.GetCardsPageAsync(cardPage, pageSize, $"set.id:{set.Id}");
                    foreach (var card in cards.Data)
                    {
                        if (!await _context.Cards.AnyAsync(c => c.Id == card.Id))
                        {
                            _context.Cards.Add(new Models.Card
                            {
                                Id = card.Id,
                                Name = card.Name,
                                Subtypes = string.Join(", ", card.Subtypes ?? []),
                                Supertype = card.Supertype,
                                Types = string.Join(", ", card.Types ?? []),
                                Rarity = card.Rarity,
                                Number = card.Number,
                                Artist = card.Artist,
                                SmallImageUrl = card.Images?.Small,
                                LargeImageUrl = card.Images?.Large,
                                SetId = set.Id,
                                TcgplayerUrl = card.TcgPlayer?.Url,
                                CardMarketUrl = card.CardMarket?.Url
                            });
                        }
                        else
                        {
                            var existingCard = await _context.Cards.FirstOrDefaultAsync(c => c.Id == card.Id);
                            existingCard.Name = card.Name;
                            existingCard.Subtypes = string.Join(", ", card.Subtypes ?? []);
                            existingCard.Supertype = card.Supertype;
                            existingCard.Types = string.Join(", ", card.Types ?? []);
                            existingCard.Rarity = card.Rarity;
                            existingCard.Number = card.Number;
                            existingCard.Artist = card.Artist;
                            existingCard.SmallImageUrl = card.Images?.Small;
                            existingCard.LargeImageUrl = card.Images?.Large;
                            existingCard.SetId = set.Id;
                            existingCard.TcgplayerUrl = card.TcgPlayer?.Url;
                            existingCard.CardMarketUrl = card.CardMarket?.Url;
                        }
                    }
                    await _context.SaveChangesAsync();
                    if (cardPage * pageSize >= cards.TotalCount) break;
                    cardPage++;
                }
            }
               
        }
    }
}
