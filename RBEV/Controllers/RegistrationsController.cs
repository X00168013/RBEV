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
    public class RegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Registrations
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Registrations.Include(r => r.Event).Include(r => r.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Registrations/Details/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Event)
                .Include(r => r.Member)
                .FirstOrDefaultAsync(m => m.RegistrationID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        [Authorize(Roles = "SuperAdmin,Moderator, Basic")]
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Events, "RacquetballEventID", "EventName");
            ViewData["MemberID"] = new SelectList(_context.Members, "ID", "FullName");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Moderator, Basic")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationID,EventID,MemberID,Division")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction("SuccessMessage");
            }
            ViewData["EventID"] = new SelectList(_context.Events, "RacquetballEventID", "EventName", registration.EventID);

            ViewData["MemberID"] = new SelectList(_context.Members, "ID", "FullName", registration.MemberID);
            return View(registration);
        }


        // GET: Registrations/Edit/5
        [Authorize(Roles = "SuperAdmin,Moderator, Basic")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Events, "RacquetballEventID", "RacquetballEventID", registration.EventID);
            ViewData["MemberID"] = new SelectList(_context.Members, "ID", "FullName", registration.MemberID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Moderator, Basic")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationID,EventID,MemberID,Division")] Registration registration)
        {
            if (id != registration.RegistrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationID))
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
            ViewData["EventID"] = new SelectList(_context.Events, "RacquetballEventID", "EventName", registration.EventID);
            ViewData["MemberID"] = new SelectList(_context.Members, "ID", "FullName", registration.MemberID);
            return View(registration);
        }


        // GET: Registrations/Delete/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Event)
                .Include(r => r.Member)
                .FirstOrDefaultAsync(m => m.RegistrationID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationID == id);
        }
        public ActionResult SuccessMessage()
        {
            return View();

        }
    }
}
