using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeedInventory.Server.Data;
using SeedInventory.Shared.Models;

namespace SeedInventory.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : Controller
    {
        private readonly AppDbContext _db;
        public SuppliersController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _db.Suppliers.ToListAsync();
        }
        // GET: api/suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
            => await _db.Suppliers.OrderBy(s => s.Name).ToListAsync();

        // GET: api/suppliers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Supplier>> Get(int id)
        {
            var s = await _db.Suppliers.FindAsync(id);
            if (s == null) return NotFound();
            return s;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<Supplier>> Create(Supplier supplier)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Suppliers.Add(supplier);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = supplier.Id }, supplier);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Supplier supplier)
        {
            if (id != supplier.Id) return BadRequest("id mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Entry(supplier).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Suppliers.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _db.Suppliers.FindAsync(id);
            if (s == null) return NotFound();
            _db.Suppliers.Remove(s);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
