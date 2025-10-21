using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Paymentscontroller : ControllerBase
    {
        private readonly GymDbContext _context;
        public Paymentscontroller(GymDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> Get() =>
            await _context.Payments.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Get(int id)
        {
            var m = await _context.Payments.FindAsync(id);
            if (m == null) return NotFound();
            return m;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> Post(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Payment payment)
        {
            if (id != payment.Id) return BadRequest();
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _context.Members.FindAsync(id);
            if (m == null) return NotFound();
            _context.Members.Remove(m);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
