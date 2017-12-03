using Gighub.Core;
using Gighub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Gighub.Controllers
{
    public class HomeController : Controller
    {


        private IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;

        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetAllUpcomingGigs(); ;

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                   g.Genre.Name.Contains(query) ||
                   g.Venue.Contains(query));
            }
            string userid = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userid)
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