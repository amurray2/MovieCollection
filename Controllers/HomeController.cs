using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

//This is the controller that works between the models and views
namespace MovieCollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MovieCollectionContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, MovieCollectionContext con)
        {
            _logger = logger;
            context = con;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPodcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        //Add movie controller method
        [HttpPost]
        public IActionResult AddMovie(Movie movieInfo)
        {
            if (ModelState.IsValid)
            {
                context.Movies.Add(movieInfo);
                context.SaveChanges();
                return RedirectToAction("AddMovie");
            }
            else
            {
                return View();
            }
        }

        //Edit movie get method
        [HttpGet]
        public IActionResult EditMovie(int movieId)
        {
            return View(context.Movies.Single(m => m.MovieId == movieId));
        }

        //Edits the movie instance in the database
        [HttpPost]
        public IActionResult EditMovie(Movie m)
        {
            var movie = context.Movies.Single(movie => movie.MovieId == m.MovieId);
            movie.category = m.category;
            movie.title = m.title;
            movie.year = m.year;
            movie.director = m.director;
            movie.rating = m.rating;
            movie.edited = m.edited;
            movie.lentTo = m.lentTo;
            movie.notes = m.notes;
            context.SaveChanges();
            return RedirectToAction("MovieList");
        }

        //deletes movie instance from the database
        [HttpPost]
        public IActionResult DeleteMovie(int movieId)
        {
            Movie movie = context.Movies.Single(m => m.MovieId == movieId);
            context.Movies.Remove(movie);
            context.SaveChanges();
            return RedirectToAction("MovieList");
        }

        [HttpGet]
        public IActionResult MovieList()
        {
            return View(context.Movies.Where(m => m.title.ToLower() != "independence day"));
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
    }
}
