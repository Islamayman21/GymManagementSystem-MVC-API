using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly GymDbContext _context;
        public TrainersController(GymDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> Get() =>
            await _context.Trainers.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> Get(int id)
        {
            var m = await _context.Trainers.FindAsync(id);
            if (m == null) return NotFound();
            return m;
        }

        [HttpPost]
        public async Task<ActionResult<Trainer>> Post(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = trainer.Id }, trainer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Trainer trainer)
        {
            if (id != trainer.Id) return BadRequest();
            _context.Entry(trainer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _context.Trainers.FindAsync(id);
            if (m == null) return NotFound();
            _context.Trainers.Remove(m);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
