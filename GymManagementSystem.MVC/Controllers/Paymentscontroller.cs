using GymMs.DAL.GymMs.DAL.Context;
using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly GymDbContext _context;

        public PaymentsController(GymDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = _context.Payments
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Member);
            return View(await payments.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (payment == null)
                return NotFound();

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewBag.Subscriptions = new SelectList(
                _context.Subscriptions.Include(s => s.Member)
                .Select(s => new {
                    s.Id,
                    DisplayName = s.Member.FullName + " - " + s.Type
                }),
                "Id", "DisplayName"
            );
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Subscriptions = new SelectList(
                _context.Subscriptions.Include(s => s.Member)
                .Select(s => new {
                    s.Id,
                    DisplayName = s.Member.FullName + " - " + s.Type
                }),
                "Id", "DisplayName", payment.SubscriptionId
            );
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            ViewBag.Subscriptions = new SelectList(
                _context.Subscriptions.Include(s => s.Member)
                .Select(s => new {
                    s.Id,
                    DisplayName = s.Member.FullName + " - " + s.Type
                }),
                "Id", "DisplayName", payment.SubscriptionId
            );
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Payment payment)
        {
            if (id != payment.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Payments.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Subscriptions = new SelectList(
                _context.Subscriptions.Include(s => s.Member)
                .Select(s => new {
                    s.Id,
                    DisplayName = s.Member.FullName + " - " + s.Type
                }),
                "Id", "DisplayName", payment.SubscriptionId
            );
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (payment == null)
                return NotFound();

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
