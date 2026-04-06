using PocketBinder.DTOs.API;
using PocketBinder.DTOs.Binder;
using PocketBinder.DTOs.Query;

namespace PocketBinder.Services.TcgApiServices
{
    public class PokemonTcgService : IPokemonTcgService
    {
        private readonly IPokemonTcgApi _pokemonTcgApi;

        public PokemonTcgService(IPokemonTcgApi pokemonTcgApi)
        {
            _pokemonTcgApi = pokemonTcgApi;
        }

        // Construye la consulta de búsqueda para cartas a partir de los parámetros del DTO
        public async Task<PaginatedResponseDto<CardSummaryDto>> GetCardsAsync(CardQueryDto query)
        {
            var q = BuildCardQuery(query);
            return await _pokemonTcgApi.GetCardsAsync(query.Page, query.PageSize, q, query.OrderBy);
        }

        // Construye la consulta de búsqueda para sets a partir de los parámetros del DTO
        public async Task<PaginatedResponseDto<CardSetDto>> GetSetsAsync(SetQueryDto query)
        {
            var q = BuildSetQuery(query);
            return await _pokemonTcgApi.GetSetsAsync(query.Page, query.PageSize, q, query.OrderBy);
        }

        // Métodos privados para construir las consultas de filtros de cartas a partir de los DTOs
        private string BuildCardQuery(CardQueryDto query)
        {
            var filters = new List<string>();
            if (!string.IsNullOrEmpty(query.Name)) filters.Add($"name:\"{query.Name}\"");
            if (!string.IsNullOrEmpty(query.SetId)) filters.Add($"set.id:{query.SetId}");
            if (!string.IsNullOrEmpty(query.Type)) filters.Add($"types:{query.Type}");
            if (!string.IsNullOrEmpty(query.Rarity)) filters.Add($"rarity:\"{query.Rarity}\"");
            if (!string.IsNullOrEmpty(query.Supertype)) filters.Add($"supertype:{query.Supertype}");
            if (!string.IsNullOrEmpty(query.Artist)) filters.Add($"artist:\"{query.Artist}\"");
            return string.Join(" ", filters);
        }

        // Métodos privados para construir las consultas de filtros de sets a partir de los DTOs
        private string BuildSetQuery(SetQueryDto query)
        {
            var filters = new List<string>();
            if (!string.IsNullOrEmpty(query.Name)) filters.Add($"name:{query.Name}");
            if (!string.IsNullOrEmpty(query.Series)) filters.Add($"series:{query.Series}");
            return string.Join(" ", filters);
        }
    }
}
