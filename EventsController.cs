using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventifyPass.Data;
using EventifyPass.Models;

namespace EventifyPass.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index() =>
            View(await _context.Events.Include(e => e.Category).ToListAsync());

        public async Task<IActionResult> Details(int id)
        {
            var @event = await _context.Events.Include(e => e.Category)
                                              .FirstOrDefaultAsync(e => e.EventId == id);
            return @event == null ? NotFound() : View(@event);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Event @event)
        {
            if (!ModelState.IsValid) return View(@event);
            _context.Add(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            return @event == null ? NotFound() : View(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Event @event)
        {
            if (!ModelState.IsValid) return View(@event);
            _context.Update(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            return @event == null ? NotFound() : View(@event);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null) _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
