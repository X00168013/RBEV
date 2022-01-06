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
using RBEV.Models.RacquetballViewModels;

namespace RBEV.Controllers
{
    public class EventCoordinatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventCoordinatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventCoordinators
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id, int? eventID)
        {
            //return View(await _context.EventCoordinators.ToListAsync());
            var viewModel = new CoordinatorIndexData();
            viewModel.EventCoordinators = await _context.EventCoordinators
                  .Include(i => i.EventAssignments)
                    .ThenInclude(i => i.Event)
                        .ThenInclude(i => i.Registrations)
                            .ThenInclude(i => i.Member)
                  .Include(i => i.EventAssignments)
                    .ThenInclude(i => i.Event)
                        .ThenInclude(i => i.Club)
                  .AsNoTracking()
                  .OrderBy(i => i.LastName)
                  .ToListAsync();

           // Events Display
            if (id != null)
            {
                ViewData["EventCoordinatorID"] = id.Value;
                EventCoordinator eventCoordinator = viewModel.EventCoordinators.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Events = eventCoordinator.EventAssignments.Select(s => s.Event);
            }

            //Registrations Display
            if (eventID != null)
            {
                ViewData["EventID"] = eventID.Value;
                viewModel.Registrations = viewModel.Events.Where(
                x => x.RacquetballEventID == eventID).Single().Registrations;
            }
            return View(viewModel);

        }


        // GET: EventCoordinators/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventCoordinator = await _context.EventCoordinators
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventCoordinator == null)
            {
                return NotFound();
            }

            return View(eventCoordinator);
        }

        // GET: EventCoordinators/Create
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventCoordinators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClubRole,ID,LastName,FirstName,Email,DOB,PhoneNumber,Address,Gender,AccountType")] EventCoordinator eventCoordinator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventCoordinator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventCoordinator);
        }

        // GET: EventCoordinators/Edit/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventCoordinator = await _context.EventCoordinators.FindAsync(id);
            if (eventCoordinator == null)
            {
                return NotFound();
            }
            return View(eventCoordinator);
        }

        // POST: EventCoordinators/Edit/5
        
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinatorToUpdate = await _context.Members.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Member>(
                coordinatorToUpdate,
                "",
                s => s.FirstName, s => s.LastName, s => s.Address, s => s.DOB, s => s.Email, s => s.PhoneNumber, s => s.Gender))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(coordinatorToUpdate);
        }

        // GET: EventCoordinators/Delete/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinator = await _context.EventCoordinators
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (coordinator == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(coordinator);
        }

        // POST: EventCoordinators/Delete/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventCoordinator = await _context.EventCoordinators.FindAsync(id);
            _context.EventCoordinators.Remove(eventCoordinator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventCoordinatorExists(int id)
        {
            return _context.EventCoordinators.Any(e => e.ID == id);
        }
    }
}
