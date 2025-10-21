using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly GymDbContext _context;
        public SubscriptionsController(GymDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> Get() =>
            await _context.Subscriptions.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> Get(int id)
        {
            var m = await _context.Subscriptions.FindAsync(id);
            if (m == null) return NotFound();
            return m;
        }

        [HttpPost]
        public async Task<ActionResult<Subscription>> Post(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = subscription.Id }, subscription);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Subscription subscription)
        {
            if (id != subscription.Id) return BadRequest();
            _context.Entry(subscription).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _context.Subscriptions.FindAsync(id);
            if (m == null) return NotFound();
            _context.Subscriptions.Remove(m);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
