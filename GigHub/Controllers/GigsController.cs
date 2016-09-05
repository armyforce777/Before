using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;


        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g=>g.Artist)
                .Include(g=>g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I am attending"
            };

            return View("Gigs",viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //var artistId = User.Identity.GetUserId();

            //Lamda expression doesn't understand the User.Identity.GetUserId() method
            //so we need to extract the value and store it to a separate variable artistId.
            //var artist = _context.Users.Single(u => u.Id == artistId);
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),

                //This below line is not a good SoC, controller should not do the job of parsing
                //a variable. So there is a INFORMATION EXPERT principal which means the class or
                //the object has the information to do sth should be one that will carry that responsibility.
                // In this case, it is viewModel who knows the Date and Time so it should be ViewModel to tackle
                // the string format

                /*
                //Metaphor: A Chef knows recipes so he is the one does the cook. -- Information Expert Principal.
                */
                
                //DateTime = DateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time)),

                //Instead create a DateTime Property to transfter date time to string
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