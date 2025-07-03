using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRMSProject.Models;

namespace PRMSProject.Controllers
{
    public class EventsController : Controller
    {
        private readonly PrmsdatabaseContext _context;

        public EventsController(PrmsdatabaseContext context)
        {
            _context = context;
        }

        // GET: Events
        [Authorize(Roles = "PropertyManager, PropertyOwner")]
        public async Task<IActionResult> Index()
        {
            var prmsdatabaseContext = _context.Events.Include(e => e.Apartment).Include(e => e.RelatedToNavigation).Include(e => e.ReportedByNavigation);
            return View(await prmsdatabaseContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Apartment)
                .Include(e => e.RelatedToNavigation)
                .Include(e => e.ReportedByNavigation)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId");
            ViewData["RelatedTo"] = new SelectList(_context.Users, "UserId", "UserPassword");
            ViewData["ReportedBy"] = new SelectList(_context.Users, "UserId", "UserPassword");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,ReportedBy,RelatedTo,ApartmentId,EventType,Description,CreatedAt")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", @event.ApartmentId);
            ViewData["RelatedTo"] = new SelectList(_context.Users, "UserId", "UserPassword", @event.RelatedTo);
            ViewData["ReportedBy"] = new SelectList(_context.Users, "UserId", "UserPassword", @event.ReportedBy);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", @event.ApartmentId);
            ViewData["RelatedTo"] = new SelectList(_context.Users, "UserId", "UserPassword", @event.RelatedTo);
            ViewData["ReportedBy"] = new SelectList(_context.Users, "UserId", "UserPassword", @event.ReportedBy);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EventId,ReportedBy,RelatedTo,ApartmentId,EventType,Description,CreatedAt")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", @event.ApartmentId);
            ViewData["RelatedTo"] = new SelectList(_context.Users, "UserId", "UserPassword", @event.RelatedTo);
            ViewData["ReportedBy"] = new SelectList(_context.Users, "UserId", "UserPassword", @event.ReportedBy);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Apartment)
                .Include(e => e.RelatedToNavigation)
                .Include(e => e.ReportedByNavigation)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(string id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
