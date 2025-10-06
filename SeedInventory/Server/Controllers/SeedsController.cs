using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeedInventory.Server.Data;
using SeedInventory.Shared.Models;

namespace SeedInventory.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedsController : ControllerBase
        {
        private readonly AppDbContext _db;
        public SeedsController(AppDbContext db) => _db = db;

        // GET: api/seeds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seed>>> GetAll()
        {
            return await _db.Seeds.OrderBy(s => s.Name).ToListAsync();
        }

        // GET: api/seeds/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Seed>> Get(int id)
        {
            var seed = await _db.Seeds.FindAsync(id);
            if (seed == null) return NotFound();
            return seed;
        }

        // POST: api/seeds
        [HttpPost]
        public async Task<ActionResult<Seed>> Create(Seed seed)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Seeds.Add(seed);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = seed.Id }, seed);
        }

        // PUT: api/seeds/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Seed seed)
        {
            if (id != seed.Id) return BadRequest("id mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Entry(seed).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Seeds.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/seeds/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var seed = await _db.Seeds.FindAsync(id);
            if (seed == null) return NotFound();

            _db.Seeds.Remove(seed);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
    }

