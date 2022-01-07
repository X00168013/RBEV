using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RBEV.Data;
using RBEV.Models;
using RBEV.Models.RacquetballViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Controllers
{
    [AllowAnonymous]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly ApplicationDbContext _context;

        public EventController(ILogger<EventController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> About()
        {
            IQueryable<GenderBreakdown> data =
                from member in _context.Members
                group member by member.Gender into genderGroup
                select new GenderBreakdown()
                {
                    Gender = genderGroup.Key,
                    MemberCount = genderGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Location()
        {
            string markers = "[";
            string conString = "Server=tcp:rbevdbserver.database.windows.net,1433;Initial Catalog=RBEV_db;Persist Security Info=False;User ID=adminserver;Password=Majella1*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            SqlCommand cmd = new SqlCommand("SELECT * FROM EventLocation");
            using (SqlConnection con = new SqlConnection(conString))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        markers += "{";
                        markers += string.Format("'title': '{0}',", sdr["Address"]);
                        markers += string.Format("'lat': '{0}',", sdr["Latitude"]);
                        markers += string.Format("'lng': '{0}',", sdr["Longitude"]);
                        markers += string.Format("'description': '{0}'", sdr["Description"]);
                        markers += "},";
                    }
                }
                con.Close();
            }

            markers += "];";
            ViewBag.Markers = markers;
            return View();
        }
    }
}
