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
            throw new NotImplementedException();
        }

        // Construye la consulta de búsqueda para sets a partir de los parámetros del DTO
        public async Task<PaginatedResponseDto<SetDto>> SearchSetsAsync(SetQueryDto query)
        {
            throw new NotImplementedException();
        }
    }
}
