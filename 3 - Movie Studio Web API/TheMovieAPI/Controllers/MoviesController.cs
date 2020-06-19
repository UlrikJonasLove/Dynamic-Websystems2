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
    public class MoviesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public MoviesController(ApiDbContext context)
        {
            _context = context;
        }

        private IEnumerable<Movies> AllMovies
        {
            get
            {
                return _context.Movie.Include(s => s.Studios).Include(r => r.Ratings).ToList();
            }
        }

        [HttpGet]
        public IEnumerable<Movies> GetAllMovies()
        {
            return AllMovies;
        }

        [HttpGet("{id}")]
        public Movies GetMovieById(int id)
        {
            return AllMovies.FirstOrDefault(x => x.Id == id);
        }

        [HttpGet("rented")]
        public IEnumerable<Movies> GetAllRentedMovies()
        {
            return AllMovies.Where(x => x.RentedOut > 0).ToList();
        }

        [HttpGet("available")]
        public IEnumerable<Movies> GetAllAvailableMovies()
        {
            return AllMovies.Where(x => x.Available && x.Count > 0).ToList();
        }

        [HttpPost("NewMovie")]
        public ActionResult PostNewMovie(Movies model)
        {
            var movie = new Movies
            {
                Name = model.Name,
                Description = model.Description,
                Count = model.Count,
                Available = model.Available,
                Trivia = model.Trivia
            };
            _context.Movie.Add(movie);
            
            try
            {
                _context.SaveChanges();
            }catch(Exception e)
            {
                return Ok(new { error = true, msg = e.Message });
            }

            return Ok(new { success = true });
        }

        [HttpPut("Status/{id}")]
        public ActionResult ChangeMovie(int id, Movies model) {

            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            if(movie != null)
            {
                movie.Available = model.Available;
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return Ok(new { error = true, msg = e.Message });
                }

                return Ok(new { success = true });
            }

            return Ok(new { error = true, msg = "Hittas ej." });
        }

        [HttpPut("{movieId}/{studioId}")]
        public ActionResult RentMoviesToStudioById (int movieId, int studioId)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == movieId);
            var filmstudio = _context.Studio.FirstOrDefault(x => x.Id == studioId);
            List<Studios> studios = new List<Studios>();
            if (movie != null && filmstudio != null)
            {
                studios = movie.Studios;
                studios.Add(filmstudio);

                movie.RentedOut += 1;
                movie.Studios = studios;
                movie.Count -= 1;
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return Ok(new { error = true, msg = e.Message });
                }

                return Ok(new { success = true });
            }

            return Ok(new { error = true, msg = "Hittas ej." });
        }

        [HttpPut("back/{id}/{studio}")]
        public ActionResult ReturnAMovieFromStudio(int id, int studio)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            var filmstudio = _context.Studio.FirstOrDefault(x => x.Id == studio);
            List<Studios> studios = new List<Studios>();
            if (movie != null && filmstudio != null)
            {
                if(movie.Studios.FirstOrDefault(x => x.Id == filmstudio.Id) != null && movie.RentedOut > 0)
                {
                    movie.Studios.Remove(filmstudio);
                    movie.RentedOut -= 1;
                    movie.Count += 1;
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Ok(new { error = true, msg = e.Message });
                    }

                    return Ok(new { success = true });
                }

            }

            return Ok(new { error = true, msg = "Hittas ej." });
        }

        [HttpPut("count/{id}/{count}")]
        public ActionResult ChangeMovieCount(int id, int count){

            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            if(movie != null)
            {
                movie.Count = count;
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return Ok(new { error = true, msg = e.Message });
                }

                return Ok(new { success = true });
            }

            return Ok(new { error = true, msg = "Hittas ej." });
        }

        [HttpPut("addtrivia/{id}")]
        public ActionResult AddTriviaToMovie(int id, Movies model)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            if (movie != null)
            {
                movie.Trivia = model.Trivia;

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return Ok(new { error = true, msg = e.Message });
                }

                return Ok(new { success = true });
            }

            return Ok(new { error = true, msg = "Hittas ej." });
        }

        [HttpPut("removetrivia/{id}")]
        public ActionResult RemoveTriviaFromMovie(int id) {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            if (movie != null)
            {
                movie.Trivia = null;
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return Ok(new { error = true, msg = e.Message });
                }
                return Ok(new { success = true });
            }

            return Ok(new { error = true, msg = "Hittas ej." });
        }

        [HttpGet("MovieRenters/{id}")]
        public IEnumerable<Studios> GetStudieRentersById(int id) { 
            return AllMovies.FirstOrDefault(x => x.Id == id).Studios.ToList();
        }

        //
    }
}
