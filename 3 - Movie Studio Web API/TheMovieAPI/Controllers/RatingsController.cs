using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheMovieAPI.Models;

namespace TheMovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RatingsController(ApiDbContext context)
        {
            _context = context;
        }

        private IEnumerable<Ratings> AllMovies
        {
            get
            {
                return _context.Rating.Include(s => s.Movie).Include(r => r.Studio).ToList();
            }
        }

        [HttpPost]
        public ActionResult Post(Ratings model)
        {
            var movie = _context.Movie.FirstOrDefault(x => x.Id == model.MovieId);
            var studio = _context.Studio.FirstOrDefault(x => x.Id == model.StudioId);
            if (movie != null && studio != null)
            {
                var rating = new Ratings
                {
                    Rating = model.Rating,
                    Movie = movie,
                    MovieId = movie.Id,
                    Studio = studio,
                    StudioId = studio.Id
                };
                _context.Rating.Add(rating);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return Ok(new { error = true, msg = e.Message });
                }
            }

            return Ok(new { error = true, msg = "Hittas ej." });
        }

    }
}
