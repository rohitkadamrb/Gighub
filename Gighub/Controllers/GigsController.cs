﻿using Gighub.Models;
using Gighub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace Gighub.Controllers
{
    public class GigsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }


        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()

            };
            return View(ViewModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();

                return View("Create", viewModel);
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}