using PocketBinder.DTOs.Binder;

namespace PocketBinder.Services.UserCollectionService
{
    public interface IUserCollectionService
    {
        Task AddCardToCollectionAsync(string CardId, int Quantity);
        Task UpdateCardQuantityAsync(string CardId, int Quantity);
        Task RemoveCardFromCollectionAsync(string CardId);
        Task<IEnumerable<CardSummaryDto>> GetUserCollectionAsync();
    }
}
