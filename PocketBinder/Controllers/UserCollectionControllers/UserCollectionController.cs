using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketBinder.DTOs.Collection;
using PocketBinder.Services.UserCollectionService;

namespace PocketBinder.Controllers.UserCollection
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCollectionController : ControllerBase
    {
        private readonly IUserCollectionService _userCollectionService;

        public UserCollectionController(IUserCollectionService userCollectionService)
        {
            _userCollectionService = userCollectionService;
        }

        [Authorize]
        [HttpPost("AddCardToCollection")]
        public async Task<IActionResult> AddCardToCollectionAsync([FromBody] AddCardToCollectionDto dto)
        {
            await _userCollectionService.AddCardToCollectionAsync(dto.CardId, dto.Quantity);
            return Ok();
        }

        [Authorize]
        [HttpGet("GetCollection")]
        public async Task<IActionResult> GetUserCollectionAsync()
        {
            var collection = await _userCollectionService.GetUserCollectionAsync();
            return Ok(collection);
        }

        [Authorize]
        [HttpDelete("{CardId}")]
        public async Task<IActionResult> RemoveCardFromCollectionAsync(string CardId)
        {
            await _userCollectionService.RemoveCardFromCollectionAsync(CardId);
            return Ok();
        }

        [Authorize]
        [HttpPut("UpdateCardQuantity")]
        public async Task<IActionResult> UpdateCardQuantityAsync([FromBody] UpdateCardFromCollectionDto dto)
        {
            await _userCollectionService.UpdateCardQuantityAsync(dto.CardId, dto.Quantity);
            return Ok();
        }
    }
}
