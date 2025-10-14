using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeedInventory.Server.Data;
using SeedInventory.Shared.Models.Setting;

namespace SeedInventory.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DistributorsController : Controller
    {
        private readonly AppDbContext _db;
        public DistributorsController(AppDbContext db) => _db = db;


        // GET: api/distributors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistributorModel>>> GetAll()
            => await _db.Distributors.OrderBy(s => s.Name).ToListAsync();

        // GET: api/distributors/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DistributorModel>> Get(int id)
        {
            var s = await _db.Distributors.FindAsync(id);
            if (s == null) return NotFound();
            return s;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<DistributorModel>> Create(DistributorModel distributor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Distributors.Add(distributor);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = distributor.Id }, distributor);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, DistributorModel distributor)
        {
            if (id != distributor.Id) return BadRequest("id mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Entry(distributor).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Distributors.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _db.Distributors.FindAsync(id);
            if (s == null) return NotFound();
            _db.Distributors.Remove(s);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
