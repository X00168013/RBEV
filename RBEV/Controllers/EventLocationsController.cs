using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RBEV.Data;
using RBEV.Models;

namespace RBEV.Controllers
{
    public class EventLocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventLocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventLocations
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventLocations.Include(e => e.Event);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "SuperAdmin,Moderator, Basic")]
        // GET: EventLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocations
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (eventLocation == null)
            {
                return NotFound();
            }

            return View(eventLocation);
        }

        // GET: EventLocations/Create
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public IActionResult Create()
        {
            PopulateEventsDropDownList();
            return View();
        }

        // POST: EventLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationID,Address,Latitude,Longitude,Description,EventID")] EventLocation eventLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["EventID"] = new SelectList(_context.Events, "RacquetballEventID", "RacquetballEventID", eventLocation.EventID);
            PopulateEventsDropDownList(eventLocation.EventID);
            return View(eventLocation);
        }

        // GET: EventLocations/Edit/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocations.FindAsync(id);
            if (eventLocation == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Events, "RacquetballEventID", "RacquetballEventID", eventLocation.EventID);
            return View(eventLocation);
        }

        // POST: EventLocations/Edit/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationID,Address,Latitude,Longitude,Description,EventID")] EventLocation eventLocation)
        {
            if (id != eventLocation.LocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventLocationExists(eventLocation.LocationID))
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
            ViewData["EventID"] = new SelectList(_context.Events, "RacquetballEventID", "RacquetballEventID", eventLocation.EventID);
            return View(eventLocation);
        }

        // GET: EventLocations/Delete/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocations
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (eventLocation == null)
            {
                return NotFound();
            }

            return View(eventLocation);
        }

        // POST: EventLocations/Delete/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventLocation = await _context.EventLocations.FindAsync(id);
            _context.EventLocations.Remove(eventLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateEventsDropDownList(object selectedEvent = null)
        {
            var eventsQuery = from d in _context.Events
                             orderby d.EventName
                             select d;
            ViewBag.EventID = new SelectList(eventsQuery.AsNoTracking(), "ClubID", "EventName", selectedEvent);

        }



        private bool EventLocationExists(int id)
        {
            return _context.EventLocations.Any(e => e.LocationID == id);
        }
    }
}
