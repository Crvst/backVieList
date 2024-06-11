using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Model;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistCanalesController : ControllerBase
    {
        private readonly ApiProjectContext _context;

        public PlaylistCanalesController(ApiProjectContext context)
        {
            _context = context;
        }

        // GET: api/PlaylistCanales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistCanales>>> GetPlaylistCanales()
        {
            return await _context.PlaylistCanales.ToListAsync();
        }

        // GET: api/PlaylistCanales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistCanales>> GetPlaylistCanales(int id)
        {
            var playlistCanales = await _context.PlaylistCanales.FindAsync(id);

            if (playlistCanales == null)
            {
                return NotFound();
            }

            return playlistCanales;
        }

        // POST: api/PlaylistCanales
        [HttpPost]
        public async Task<ActionResult<PlaylistCanales>> PostPlaylistCanales(PlaylistCanales playlistCanales)
        {
            Playlist playlist = await _context.Playlist.FindAsync(playlistCanales.PlaylistId);
            Canal canal = await _context.Canal.FindAsync(playlistCanales.CanalId);

            playlistCanales.Canal = canal;
            playlistCanales.Playlist = playlist;

            _context.PlaylistCanales.Add(playlistCanales);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlaylistCanales), new { id = playlistCanales.PlaylistId }, playlistCanales);
        }

        // DELETE: api/PlaylistCanales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylistCanales(int id)
        {
            var playlistCanales = await _context.PlaylistCanales.FindAsync(id);
            if (playlistCanales == null)
            {
                return NotFound();
            }

            _context.PlaylistCanales.Remove(playlistCanales);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaylistCanalesExists(int id)
        {
            return _context.PlaylistCanales.Any(e => e.PlaylistId == id);
        }
    }
}
