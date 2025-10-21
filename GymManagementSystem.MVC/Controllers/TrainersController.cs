using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class TrainersController : Controller
    {
        private readonly GymDbContext _context;

        public TrainersController(GymDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.ToListAsync();
            return View(trainers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trainer trainer)
        {
            if (id != trainer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Trainers.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
