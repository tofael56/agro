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
    public class CompanyController : Controller
    {
        private readonly AppDbContext _db;
        public CompanyController(AppDbContext db) => _db = db;


        // GET: api/company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetAll()
            => await _db.Company.OrderBy(s => s.Name).ToListAsync();

        // GET: api/company/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompanyModel>> Get(int id)
        {
            var s = await _db.Company.FindAsync(id);
            if (s == null) return NotFound();
            return s;
        }

        // POST
        [HttpPost]  
        public async Task<ActionResult<CompanyModel>> Create(CompanyModel company)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Company.Add(company);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CompanyModel company)
        {
            if (id != company.Id) return BadRequest("id mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Entry(company).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Company.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _db.Company.FindAsync(id);
            if (s == null) return NotFound();
            _db.Company.Remove(s);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
