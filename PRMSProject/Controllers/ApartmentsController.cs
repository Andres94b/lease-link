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
    public class ApartmentsController : Controller
    {
        private readonly PrmsdatabaseContext _context;

        public ApartmentsController(PrmsdatabaseContext context)
        {
            _context = context;
        }

        // GET: Apartments
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Index()
        {
            var prmsdatabaseContext = _context.Apartments.Include(a => a.Building).Include(a => a.Owner).Include(a => a.Tenant).Include(a => a.Manager);
            return View(await prmsdatabaseContext.ToListAsync());
        }

        // GET: Apartments/List/5
        [Authorize(Roles = "PropertyManager")]
        public async Task<IActionResult> List(int id)
        {
            var prmsdatabaseContext = _context.Apartments.Include(a => a.Building).Include(a => a.Owner).Include(a => a.Tenant).Where(a => a.ManagerId==id);
            return View(await prmsdatabaseContext.ToListAsync());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Building)
                .Include(a => a.Owner)
                .Include(a => a.Tenant)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId");
            ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserPassword");
            ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "UserPassword");
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserFullName");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,BuildingId,OwnerId,TenantId,ManagerId,ApartmentNumber,ApartmentInterior,FloorNumber,Area,BedroomAmount,BathroomsAmount,GarageAmount,DepositAmount,BalconyAmount,RoomAmount,IsRented,PicturesPath")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartment.BuildingId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserPassword", apartment.OwnerId);
            ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "UserPassword", apartment.TenantId);//need to change the passwords
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserFullName", apartment.ManagerId);

            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartment.BuildingId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserPassword", apartment.OwnerId);
            ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "UserPassword", apartment.TenantId);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ApartmentId,BuildingId,OwnerId,TenantId,ApartmentNumber,ApartmentInterior,FloorNumber,Area,BedroomAmount,BathroomsAmount,GarageAmount,DepositAmount,BalconyAmount,RoomAmount,IsRented,PicturesPath")] Apartment apartment)
        {
            if (id != apartment.ApartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ApartmentId))
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
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartment.BuildingId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserPassword", apartment.OwnerId);
            ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "UserPassword", apartment.TenantId);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Building)
                .Include(a => a.Owner)
                .Include(a => a.Tenant)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(string id)
        {
            return _context.Apartments.Any(e => e.ApartmentId == id);
        }
    }
}
