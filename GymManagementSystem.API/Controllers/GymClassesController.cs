using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymClassesController : ControllerBase
    {
        private readonly GymDbContext _context;
        public GymClassesController(GymDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymClass>>> Get() =>
            await _context.Classes.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<GymClass>> Get(int id)
        {
            var m = await _context.Classes.FindAsync(id);
            if (m == null) return NotFound();
            return m;
        }

        [HttpPost]
        public async Task<ActionResult<GymClass>> Post(GymClass gymClass)
        {
            _context.Classes.Add(gymClass);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = gymClass.Id }, gymClass);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, GymClass gymClass)
        {
            if (id != gymClass.Id) return BadRequest();
            _context.Entry(gymClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _context.Classes.FindAsync(id);
            if (m == null) return NotFound();
            _context.Classes.Remove(m);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
