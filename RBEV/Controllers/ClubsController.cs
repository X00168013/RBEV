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
    public class ClubsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string appErrorPath = "Views/Shared/AppError.cshtml";


        public ClubsController(ApplicationDbContext context)
        {
            _context = context;
        }
  

        [AllowAnonymous]
        // GET: Events/Details/5

        //GET :  Search Clubs
        public async Task<IActionResult> SearchClubs(string searchString)
        {

            //Search box
            var clubs = from s in _context.Clubs
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clubs = clubs.Where(s => s.Name.Contains(searchString));
            }

            return View(await clubs.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Clubs
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CountySortParm"] = sortOrder == "County" ? "county_desc" : "County";
            ViewData["ProvinceSortParm"] = sortOrder == "Province" ? "province_desc" : "Province";
            //Paginated List
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //Search box
            var clubs = from s in _context.Clubs
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clubs = clubs.Where(s => s.Name.Contains(searchString));
            }
            //Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    clubs = clubs.OrderByDescending(s => s.Name);
                    break;
                case "County":
                    clubs = clubs.OrderBy(s => s.County);
                    break;
                case "county_desc":
                    clubs = clubs.OrderByDescending(s => s.County);
                    break;
                case "Province":
                    clubs = clubs.OrderBy(s => s.Province);
                    break;
                case "province_desc":
                    clubs = clubs.OrderByDescending(s => s.Province);
                    break;
                default:
                    clubs = clubs.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            //var racquetballContext = _context.Clubs.Include(c => c.Adminstrator);
            return View(await PaginatedList<Club>.CreateAsync(clubs.AsNoTracking().Include(c => c.Adminstrator), pageNumber ?? 1, pageSize));
            //return View(await racquetballContext.ToListAsync());
        }


        [AllowAnonymous]
        // GET: Clubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .Include(i => i.Adminstrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ClubID == id);


            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }


        // GET: Clubs/Create
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public IActionResult Create()
        {
            ViewData["EventCoordinatorID"] = new SelectList(_context.EventCoordinators, "ID", "FullName");
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClubID,Name,County,Province,NumberofCourts,EventCoordinatorID,RowVersion")] Club club)
        {
            if (ModelState.IsValid)
            {
                _context.Add(club);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventCoordinatorID"] = new SelectList(_context.EventCoordinators, "ID", "FullName", club.EventCoordinatorID);
            return View(club);
        }

        // GET: Clubs/Edit/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .Include(i => i.Adminstrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ClubID == id);

            if (club == null)
            {
                return NotFound();
            }
            ViewData["EventCoordinatorID"] = new SelectList(_context.EventCoordinators, "ID", "FullName", club.EventCoordinatorID);
            return View(club);
        }

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion /*, [Bind("ClubID,Name,County,Province,NumberofCourts,EventCoordinatorID,RowVersion")] Club club*/)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clubToUpdate = await _context.Clubs.Include(i => i.Adminstrator).FirstOrDefaultAsync(m => m.ClubID == id);

            if (clubToUpdate == null)
            {
                Club deletedClub = new Club();
                await TryUpdateModelAsync(deletedClub);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The club was deleted by another user.");
                ViewData["EventCoordinatorID"] = new SelectList(_context.EventCoordinators, "ID", "FullName", deletedClub.EventCoordinatorID);
                return View(deletedClub);
            }

            _context.Entry(clubToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Club>(
                clubToUpdate,
                "",
                s => s.Name, s => s.County, s => s.Province, s => s.EventCoordinatorID, s => s.NumberofCourts))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)

                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Club)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The club was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Club)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                        }
                        if (databaseValues.County != clientValues.County)
                        {
                            ModelState.AddModelError("County", $"Current value: {databaseValues.County:c}");
                        }
                        if (databaseValues.Province != clientValues.Province)
                        {
                            ModelState.AddModelError("Province", $"Current value: {databaseValues.Province:d}");
                        }
                        if (databaseValues.EventCoordinatorID != clientValues.EventCoordinatorID)
                        {
                            EventCoordinator databaseEventCoordinator = await _context.EventCoordinators.FirstOrDefaultAsync(i => i.ID == databaseValues.EventCoordinatorID);
                            ModelState.AddModelError("InstructorID", $"Current value: {databaseEventCoordinator?.FullName}");
                        }
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                               + "was modified by another user after you got the original value. The "
                                               + "edit operation was canceled and the current values in the database "
                                               + "have been displayed. If you still want to edit this record, click "
                                               + "the Save button again. Otherwise click the Back to List hyperlink.");
                        clubToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["EventCoordinatorID"] = new SelectList(_context.EventCoordinators, "ID", "FullName", clubToUpdate.EventCoordinatorID);
            return View(clubToUpdate);
        }

        // GET: Clubs/Delete/5
        [Authorize(Roles = "SuperAdmin,Moderator")]
        public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .Include(c => c.Adminstrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ClubID == id);
            if (club == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The Club record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SuperAdmin,Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Club club)
        {
            try
            {
                if (await _context.Clubs.AnyAsync(m => m.ClubID == club.ClubID))
                {
                    _context.Clubs.Remove(club);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { concurrencyError = true, id = club.ClubID });
            }
        }

        private bool ClubExists(int id)
        {
            return _context.Clubs.Any(e => e.ClubID == id);
        }
    }
}
