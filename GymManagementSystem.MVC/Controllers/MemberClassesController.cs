using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class MemberClassesController : Controller
    {
        private readonly GymDbContext _context;

        public MemberClassesController(GymDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var memberClasses = await _context.MemberClasses
                .Include(mc => mc.Member)
                .Include(mc => mc.GymClass)
                .ToListAsync();
            return View(memberClasses);
        }

        public async Task<IActionResult> Details(int memberId, int gymClassId)
        {
            var memberClass = await _context.MemberClasses
                .Include(mc => mc.Member)
                .Include(mc => mc.GymClass)
                .FirstOrDefaultAsync(mc => mc.MemberId == memberId && mc.GymClassId == gymClassId);

            if (memberClass == null) return NotFound();
            return View(memberClass);
        }

        public IActionResult Create()
        {
            ViewBag.Members = new SelectList(_context.Members, "Id", "FullName");
            ViewBag.Classes = new SelectList(_context.Classes, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberClass memberClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Members = new SelectList(_context.Members, "Id", "FullName", memberClass.MemberId);
            ViewBag.Classes = new SelectList(_context.Classes, "Id", "Title", memberClass.GymClassId);
            return View(memberClass);
        }

        public async Task<IActionResult> Delete(int memberId, int gymClassId)
        {
            var memberClass = await _context.MemberClasses
                .Include(mc => mc.Member)
                .Include(mc => mc.GymClass)
                .FirstOrDefaultAsync(mc => mc.MemberId == memberId && mc.GymClassId == gymClassId);

            if (memberClass == null) return NotFound();
            return View(memberClass);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int memberId, int gymClassId)
        {
            var memberClass = await _context.MemberClasses
                .FirstOrDefaultAsync(mc => mc.MemberId == memberId && mc.GymClassId == gymClassId);

            if (memberClass != null)
            {
                _context.MemberClasses.Remove(memberClass);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
