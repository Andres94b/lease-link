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
    public class RequestsController : Controller
    {
        private readonly PrmsdatabaseContext _context;

        public RequestsController(PrmsdatabaseContext context)
        {
            _context = context;
        }

        // GET: Requests
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var prmsdatabaseContext = _context.Requests.Include(r => r.Apartment).Include(r => r.User);
            return View(await prmsdatabaseContext.ToListAsync());
        }

        // GET: Requests
        [Authorize(Roles = "Tenant, PropertyManager")]
        public async Task<IActionResult> List(int id)
        {
            var prmsdatabaseContext = _context.Requests.Include(r => r.Apartment).Include(r => r.User).Where(r => r.UserId==id || r.Apartment.ManagerId==id );
            return View(await prmsdatabaseContext.ToListAsync());
        }


        // GET: Requests/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Apartment)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        //public IActionResult Create()
        //{
        //    ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId");
        //    ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
        //    return View();
        //}
        // GET: Requests/Create/5
        public async Task<IActionResult> Create(int id)
        {
            var tenant = await _context.Users
                .Include(u=>u.ApartmentTenants)
                .ThenInclude(a => a.Manager)
                .FirstOrDefaultAsync(a=>a.UserId==id);
            if (tenant == null)
            {
                return NotFound();
            }
            string apartmentId = tenant.ApartmentTenants.FirstOrDefault().ApartmentId;

            ViewData["ApartmentId"] = apartmentId;
            ViewData["ManagerName"] = tenant.ApartmentTenants.FirstOrDefault().Manager.UserFullName;
            ViewData["UserId"] = id;
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,UserId,ApartmentId,RequestText,RequestStatus,CreatedAt")] Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List), new {id = request.UserId});
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", request.ApartmentId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", request.UserId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", request.ApartmentId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserFullName", request.UserId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RequestId,UserId,ApartmentId,RequestText,RequestStatus,CreatedAt")] Request request)
        {
            if (id != request.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.RequestId))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", request.ApartmentId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", request.UserId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Apartment)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(string id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }
    }
}
