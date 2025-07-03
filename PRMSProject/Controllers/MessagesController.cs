using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRMSProject.Models;

namespace PRMSProject.Controllers
{
    public class MessagesController : Controller
    {
        private readonly PrmsdatabaseContext _context;

        public MessagesController(PrmsdatabaseContext context)
        {
            _context = context;
        }

        // GET: Messages
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var prmsdatabaseContext = _context.Messages.Include(m => m.Receiver).Include(m => m.Sender);
            return View(await prmsdatabaseContext.ToListAsync());
        }

        // GET: Messages/List/5/tab=inbox
        [Authorize]
        public async Task<IActionResult> List(int id, string tab ="Inbox")
        {

            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (id != currentUserId)
            {
                return Forbid(); // or RedirectToAction("AccessDenied", "Account")
            }

            IQueryable<Message> prmsdatabaseContext;

            if (tab == "Sent")
            {
                prmsdatabaseContext = _context.Messages.Include(m => m.Receiver).Where(m => m.SenderId == id);
            }
            else
            {
                prmsdatabaseContext = _context.Messages.Include(m => m.Sender).Where(m => m.ReceiverId == id);
            }

            ViewData["tab"] = tab;
            ViewData["id"] = id;

            return View(await prmsdatabaseContext.ToListAsync());

        }

        // GET: Messages/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        [Authorize]
        public async Task<IActionResult> Create(string? apartmentId)
        {
            if(apartmentId != null) {
                var listing = await _context.Listings
                    .Include(l => l.Apartment)
                    .ThenInclude(a=>a.Manager)
                    .FirstOrDefaultAsync(l => l.ApartmentId == apartmentId);
                ViewData["ApartmentManagerName"] = listing.Apartment.Manager.UserFullName;
                ViewData["ApartmentManagerId"] = listing.Apartment.ManagerId.ToString();
                ViewData["ApartmentId"] = listing.ApartmentId.ToString();
            }
            ViewData["ReceiverId"] = new SelectList(_context.Users, "UserId", "UserFullName");
            ViewData["SenderId"] = new SelectList(_context.Users, "UserId", "UserFullName");
            return View();

        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("MessageId,SenderId,ReceiverId,MessageSubject,MessageText")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.SentAt = DateTime.Now;

                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { id=message.SenderId, tab="Inbox"});
            }
            ViewData["ReceiverId"] = new SelectList(_context.Users, "UserId", "UserFullName", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "UserId", "UserFullName", message.SenderId);
            return View(message);
        }

        // GET: Messages/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["ReceiverId"] = new SelectList(_context.Users, "UserId", "UserPassword", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "UserId", "UserPassword", message.SenderId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(string id, [Bind("MessageId,SenderId,ReceiverId,MessageSubject,MessageText,SentAt")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
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
            ViewData["ReceiverId"] = new SelectList(_context.Users, "UserId", "UserPassword", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "UserId", "UserPassword", message.SenderId);
            return View(message);
        }

        // GET: Messages/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(string id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
