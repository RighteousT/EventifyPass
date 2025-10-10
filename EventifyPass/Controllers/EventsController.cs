using EventifyPass.Data;
using EventifyPass.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EventifyPass.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //Events
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.Include(e => e.Category).ToListAsync();
            return View(events);
        }

        //Events' Details
        public async Task<IActionResult> Details(int id)
        {
            var @event = await _context.Events.Include(e => e.Category)
                                              .FirstOrDefaultAsync(e => e.EventId == id);
            return @event == null ? NotFound() : View(@event);
        }

        //Events' Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Events' Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Handles the optional image upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Path.GetFileNameWithoutExtension(imageFile.FileName)
                        + "_" + Guid.NewGuid().ToString()
                        + Path.GetExtension(imageFile.FileName);

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    @event.ImagePath = "/uploads/" + uniqueFileName;
                }

                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", @event.CategoryId);
            return View(@event);
        }

        // Events' Editing
        public async Task<IActionResult> Edit(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
                return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", @event.CategoryId);
            return View(@event);
        }

        // POST: Events' Editing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event, IFormFile? imageFile)
        {
            if (id != @event.EventId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //Replaces or keeps image
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadsFolder);

                        string uniqueFileName = Path.GetFileNameWithoutExtension(imageFile.FileName)
                            + "_" + Guid.NewGuid().ToString()
                            + Path.GetExtension(imageFile.FileName);

                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        @event.ImagePath = "/uploads/" + uniqueFileName;
                    }
                    else
                    {
                        
                        var existingEvent = await _context.Events.AsNoTracking()
                            .FirstOrDefaultAsync(e => e.EventId == id);
                        @event.ImagePath = existingEvent?.ImagePath;
                    }

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Events.Any(e => e.EventId == @event.EventId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", @event.CategoryId);
            return View(@event);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            return @event == null ? NotFound() : View(@event);
        }

        // POST: Delete Confirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
                _context.Events.Remove(@event);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
