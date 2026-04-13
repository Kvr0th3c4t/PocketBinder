using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocketBinder.DTOs.Album;
using PocketBinder.Models;
using PocketBinder.Services.AlbumService;

namespace PocketBinder.Controllers.AlbumControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAlbumAsync([FromBody] CreateAlbumDto dto)
        {
            await _albumService.CreateAlbumAsync(dto);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserAlbumsAsync()
        {
            var userAlbums = await _albumService.GetUserAlbumsAsync();
            return Ok(userAlbums);
        }

        [Authorize]
        [HttpGet("{albumId}")]
        public async Task<IActionResult> GetAlbumDetailAsync([FromRoute] int albumId)
        {
            var albumDetail = await _albumService.GetAlbumDetailAsync(albumId);
            return Ok(albumDetail);
        }

        [Authorize]
        [HttpDelete("{albumId}")]
        public async Task<IActionResult> DeleteAlbumAsync([FromRoute] int albumId)
        {
            await _albumService.DeleteAlbumAsync(albumId);
            return Ok();
        }

        [Authorize]
        [HttpPost("{albumId}/cards")]
        public async Task<IActionResult> AddCardToAlbumAsync([FromRoute] int albumId, [FromBody] string cardId)
        {
            await _albumService.AddCardToAlbumAsync(albumId, cardId);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{albumId}/cards/{cardId}")]
        public async Task<IActionResult> RemoveCardFromAlbumAsync([FromRoute] int albumId, [FromRoute] string cardId)
        {
            await _albumService.RemoveCardFromAlbumAsync(albumId, cardId);
            return Ok();
        }
    }
}
