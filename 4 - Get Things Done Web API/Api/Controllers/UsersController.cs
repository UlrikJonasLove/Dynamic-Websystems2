using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UsersController(ApiDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> AllUsers
        {
            get
            {
                return _context.Users.ToList();
            }
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return AllUsers;
        }

        [HttpDelete("DeleteMemberFromTodo/{id}/Member/{userId}")]
        public ActionResult Delete(int id, string userId)
        {
            var user = AllUsers.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var todos = _context.TodoItems.Include("Members").FirstOrDefault(x => x.Members.Any(m => m.Id == user.Id) && x.Id == id);

                if (todos != null)
                    todos.Members.Remove(user);

                if (_context.SaveChanges() > 0)
                    return Ok(true);
            }
            return Ok(false);
        }

        [HttpDelete("DeleteAccount")]
        public ActionResult Delete()
        {
            var userId = User.Claims.ToList()[0].Value;
            var user = AllUsers.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var list = _context.TodoItems.Include("Members").Where(x => x.Author.Id == userId).ToList();
                for (var i = 0; i < list.Count; i++)
                {
                    _context.TodoItems.Remove(list[i]);
                    if (_context.SaveChanges() == 0)
                        return Ok(false);
                }

                var member = _context.TodoItems.Include("Members").Where(x => x.Members.Any(m => m.Id == user.Id)).ToList();
                if (member.Count > 0)
                {
                    for (var i = 0; i < member.Count; i++)
                    {
                        list[i].Members.Remove(user);
                        if (_context.SaveChanges() == 0)
                            return Ok(false);
                    }
                }


                _context.Users.Remove(user);
                if (_context.SaveChanges() > 0)
                    return Ok(true);
            }
            return Ok(false);
        }
    }
}
