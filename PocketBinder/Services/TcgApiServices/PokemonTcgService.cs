using Microsoft.EntityFrameworkCore;
using PocketBinder.Data;
using PocketBinder.DTOs.Binder;
using PocketBinder.DTOs.Pagination;
using PocketBinder.DTOs.Query;

namespace PocketBinder.Services.TcgApiServices
{
    public class PokemonTcgService : IPokemonTcgService
    {
        private readonly ApplicationDbContext _context;
        public PokemonTcgService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Construye la consulta de búsqueda para cartas a partir de los parámetros del DTO
        public async Task<PaginatedResponseDto<CardSummaryDto>> SearchCardsAsync(CardQueryDto query)
        {
            var cardsQuery = _context.Cards.Include(c => c.Set).AsQueryable(); ;

            if (!string.IsNullOrEmpty(query.Name))
            {
                cardsQuery = cardsQuery.Where(c => c.Name.Contains(query.Name));
            }
            if (!string.IsNullOrEmpty(query.SetId))
            {
                cardsQuery = cardsQuery.Where(c => c.SetId == query.SetId);
            }
            if (!string.IsNullOrEmpty(query.Type))
            {
                cardsQuery = cardsQuery.Where(c => c.Types.Contains(query.Type));
            }
            if (!string.IsNullOrEmpty(query.Rarity))
            {
                cardsQuery = cardsQuery.Where(c => c.Rarity == query.Rarity);
            }
            if (!string.IsNullOrEmpty(query.Supertype))
            {
                cardsQuery = cardsQuery.Where(c => c.Supertype == query.Supertype);
            }
            if (!string.IsNullOrEmpty(query.Artist))
            {
                cardsQuery = cardsQuery.Where(c => c.Artist.Contains(query.Artist));
            }
            var totalCount = await cardsQuery.CountAsync();
            cardsQuery = query.OrderBy switch
            {
                "name" => cardsQuery.OrderBy(c => c.Name),
                "-name" => cardsQuery.OrderByDescending(c => c.Name),
                "number" => cardsQuery.OrderBy(c => c.Number),
                "-number" => cardsQuery.OrderByDescending(c => c.Number),
                "set" => cardsQuery.OrderBy(c => c.Set.Name),
                "-set" => cardsQuery.OrderByDescending(c => c.Set.Name),
                _ => cardsQuery.OrderBy(c => c.Number) 
            };
            var data = await cardsQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(c => new CardSummaryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    SetId = c.SetId,
                    SetName = c.Set.Name,
                    Number = c.Number,
                    Rarity = c.Rarity,
                    SmallImageUrl = c.SmallImageUrl
                })
                .ToListAsync();
            return new PaginatedResponseDto<CardSummaryDto>
            {
                Data = data,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };
        }

        // Construye la consulta de búsqueda para sets a partir de los parámetros del DTO
        public async Task<PaginatedResponseDto<SetDto>> SearchSetsAsync(SetQueryDto query)
        {
            var setsQuery = _context.Sets.AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
            {
                setsQuery = setsQuery.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrEmpty(query.Series))
            {
                setsQuery = setsQuery.Where(s => s.Series.Contains(query.Series));
            }
            var totalCount = await setsQuery.CountAsync();
            setsQuery = query.OrderBy switch
            {
                "name" => setsQuery.OrderBy(c => c.Name),
                "-name" => setsQuery.OrderByDescending(c => c.Name),
                "releaseDate" => setsQuery.OrderBy(c => c.ReleaseDate),
                "-releaseDate" => setsQuery.OrderByDescending(c => c.ReleaseDate),
                "total" => setsQuery.OrderBy(c => c.Total),
                "-total" => setsQuery.OrderByDescending(c => c.Total),
                _ => setsQuery.OrderBy(c => c.Name)
            };
            var data = await setsQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(s => new SetDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Series = s.Series,
                    PrintedTotal = s.PrintedTotal,
                    Total = s.Total,
                    ReleaseDate = s.ReleaseDate,
                    SymbolUrl = s.SymbolUrl,
                    LogoUrl = s.LogoUrl
                })
                .ToListAsync();
            return new PaginatedResponseDto<SetDto>
            {
                Data = data,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
