using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly GymDbContext _context;
        public MembersController(GymDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> Get() =>
            await _context.Members.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> Get(int id)
        {
            var m = await _context.Members.FindAsync(id);
            if (m == null) return NotFound();
            return m;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> Post(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Member member)
        {
            if (id != member.Id) return BadRequest();
            _context.Entry(member).State = EntityState.Modified;
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
