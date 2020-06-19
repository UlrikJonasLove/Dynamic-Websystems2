using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMovieAPI.Models;

namespace TheMovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudiosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public StudiosController(ApiDbContext context)
        {
            _context = context;
        }
        private bool Studios(long id)
        {
            return _context.Studio.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studios>>> GetAllStudios()
        {
            return await _context.Studio.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Studios>> GetStudioById(long id)
        {
            var studios = await _context.Studio.FindAsync(id);

            if (studios == null)
            {
                return NotFound();
            }

            return studios;
        }

        [HttpPost]
        public async Task<ActionResult<Studios>> PostStudios(Studios studio)
        {
            _context.Studio.Add(studio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllStudios), new { id = studio.Id }, studio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudioById(long id, Studios studio)
        {
            if (id != studio.Id)
            {
                return BadRequest();
            }

            _context.Entry(studio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Studios(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Studios>> DeleteStudioById(long id)
        {
            var studios = await _context.Studio.FindAsync(id);
            if (studios == null)
            {
                return NotFound();
            }

            _context.Studio.Remove(studios);
            await _context.SaveChangesAsync();

            return studios;
        }
    }
}
