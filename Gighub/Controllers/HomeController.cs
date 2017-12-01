using Gighub.Models;
using Gighub.Repositories;
using Gighub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Gighub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private AttendanceRepository _attendanceRepository; public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCancelled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                   g.Genre.Name.Contains(query) ||
                   g.Venue.Contains(query));
            }
            string userid = User.Identity.GetUserId();
            var attendances = _attendanceRepository.GetFutureAttendances(userid)
                .ToLookup(a => a.GigId);
            //var following = _context.Followings
            //        .Where(f => f.FollowerId == userid)
            //        .ToList()
            //        .ToLookup(f => f.FolloweeId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances,
                //Followings = following
            };
            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}