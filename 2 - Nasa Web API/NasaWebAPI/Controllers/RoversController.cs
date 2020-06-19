using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NasaWebAPI.Models;

namespace NasaWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoversController : ControllerBase
    {
        private readonly ILogger<RoversController> _logger;
        private readonly IRoverRepository _rover;

        public RoversController(ILogger<RoversController> logger, IRoverRepository rover)
        {
            _logger = logger;
            _rover = rover;
        }

        [HttpGet]
        public IEnumerable<Rovers> GetRovers() //hämtar alla rovers samt kategorier
        {
            return _rover.Rovers.ToList();
        }

        [HttpGet("{id}")] //endpoint för id
        public Rovers GetRover(int id)
        {
            return _rover.GetRover(id);
        }

        [HttpGet("category/{id}")] //endpoint för kategori id
        public IEnumerable<Rovers> GetRoverByCategory(int id)
        {
            var list = _rover.Rovers.ToList();
            return list.Where(C => C.Category.CategoryId == id).ToList();
        }

        [HttpGet("speed/{id}")] //endpoint för hastighet
        public IEnumerable<Rovers> GetRoverBySpeed(int id)
        {
            var list = _rover.Rovers.ToList();
            return list.Where(s => s.Speed > id).ToList();
        }

        [HttpGet("wheels/{id}")] //endpoint för hjul
        public IEnumerable<Rovers> GetRoversByWheelsCount(int id)
        {
            var list = _rover.Rovers.ToList();
            return list.Where(w => w.Wheels == id).ToList();
        }
    }
}
