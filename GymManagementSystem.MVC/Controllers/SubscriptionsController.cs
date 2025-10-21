using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly GymDbContext _context;

        public SubscriptionsController(GymDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var subscriptions = await _context.Subscriptions
                .Include(s => s.Member)
                .ToListAsync();
            return View(subscriptions);
        }

        public async Task<IActionResult> Details(int id)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.Member)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subscription == null) return NotFound();
            return View(subscription);
        }

        public IActionResult Create()
        {
            ViewBag.Members = new SelectList(_context.Members, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Members = new SelectList(_context.Members, "Id", "FullName", subscription.MemberId);
            return View(subscription);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null) return NotFound();

            ViewBag.Members = new SelectList(_context.Members, "Id", "FullName", subscription.MemberId);
            return View(subscription);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subscription subscription)
        {
            if (id != subscription.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Subscriptions.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Members = new SelectList(_context.Members, "Id", "FullName", subscription.MemberId);
            return View(subscription);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.Member)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subscription == null) return NotFound();
            return View(subscription);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
