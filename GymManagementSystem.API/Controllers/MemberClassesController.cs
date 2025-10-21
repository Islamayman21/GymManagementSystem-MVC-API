using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberClassesController : ControllerBase
    {
        private readonly GymDbContext _context;
        public MemberClassesController(GymDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberClass>>> Get() =>
            await _context.MemberClasses.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberClass>> Get(int id)
        {
            var m = await _context.MemberClasses.FindAsync(id);
            if (m == null) return NotFound();
            return m;
        }

        [HttpPost]
        public async Task<ActionResult<MemberClass>> Post(MemberClass memberClass)
        {
            _context.MemberClasses.Add(memberClass);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = memberClass.MemberId }, memberClass);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MemberClass memberClass)
        {
            if (id != memberClass.MemberId) return BadRequest();
            _context.Entry(memberClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _context.MemberClasses.FindAsync(id);
            if (m == null) return NotFound();
            _context.MemberClasses.Remove(m);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
