using Gighub.Core;
using Gighub.Core.Models;
using Gighub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace Gighub.Controllers
{
    public class GigsController : Controller
    {






        private readonly IUnitOfWork _UnitOfWork;

        public GigsController(IUnitOfWork unitofWork)
        {





            _UnitOfWork = unitofWork;
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _UnitOfWork.Gigs.GetGigOfCurrentUser(userId);
            return View(gigs);
        }


        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();


            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _UnitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I 'm Attending",
                Attendances = _UnitOfWork.Attendances.GetFutureAttendances(userId)
                                .ToLookup(a => a.GigId),

            };
            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new GigFormViewModel
            {
                Genres = _UnitOfWork.Genres.GetAllGenres(),
                Heading = "Add a Gig"


            };
            return View("GigForm", ViewModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _UnitOfWork.Genres.GetAllGenres();

                return View("GigForm", viewModel);
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };
            _UnitOfWork.Gigs.Add(gig);

            _UnitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");

        }
        [Authorize]
        public ActionResult Edit(int id)
        {

            var gig = _UnitOfWork.Gigs.GetGigWithArtistAndGenre(id);
            if (gig == null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();
            var ViewModel = new GigFormViewModel
            {
                Genres = _UnitOfWork.Genres.GetAllGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Id = gig.Id,
                Heading = " edit a gig"
            };
            return View("GigForm", ViewModel);

        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _UnitOfWork.Genres.GetAllGenres();

                return View("GigForm", viewModel);
            }
            var gig = _UnitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();
            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _UnitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");

        }
        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {

            var gig = _UnitOfWork.Gigs.GetGigWithArtistAndGenre(Id);

            if (gig == null)
            {
                return HttpNotFound();
            }


            var viewModel = new GigDetailsViewModel
            {
                Gig = gig
            };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsGoing = _UnitOfWork.Attendances.GetIfAnyAttendance(gig.Id, userId) != null;
                viewModel.IsFollowing = _UnitOfWork.Followings.GetAnyFollowings(gig.ArtistId, userId) != null;

            }


            return View("Details", viewModel);
        }

    }
}