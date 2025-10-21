using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class ClassesController : Controller
    {
        private readonly GymDbContext _context;

        public ClassesController(GymDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes
                .Include(c => c.Trainer) 
                .ToListAsync();

            return View(classes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var gymClass = await _context.Classes
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (gymClass == null) return NotFound();

            return View(gymClass);
        }

        public IActionResult Create()
        {
            ViewBag.Trainers = new SelectList(_context.Trainers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Trainers = new SelectList(_context.Trainers, "Id", "FullName", gymClass.TrainerId);
            return View(gymClass);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var gymClass = await _context.Classes.FindAsync(id);
            if (gymClass == null) return NotFound();

            ViewBag.Trainers = new SelectList(_context.Trainers, "Id", "FullName", gymClass.TrainerId);
            return View(gymClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GymClass gymClass)
        {
            if (id != gymClass.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Classes.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Trainers = new SelectList(_context.Trainers, "Id", "FullName", gymClass.TrainerId);
            return View(gymClass);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var gymClass = await _context.Classes
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (gymClass == null) return NotFound();

            return View(gymClass);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await _context.Classes.FindAsync(id);
            if (gymClass != null)
            {
                _context.Classes.Remove(gymClass);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
