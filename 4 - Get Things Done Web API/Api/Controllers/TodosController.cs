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
    public class TodosController : ControllerBase
    {
            private readonly ApiDbContext _context;
            public TodosController(ApiDbContext context)
            {
                _context = context;
            }

            public IEnumerable<TodoModel> AllTodos
            {
                get
                {
                    return _context.TodoItems.Include("Author").Include("Members").ToList();
                }
            }

            public IEnumerable<Tags> AllTags
            {
                get
                {
                    return _context.TagsItems.ToList();
                }
            }
 
            [HttpGet]
            public IEnumerable<TodoModel> Get()
            {
                var userId = User.Claims.ToList()[0].Value;
                return AllTodos.Where(x => x.Author.Id == userId).ToList();
            }

            [HttpGet("Member")]
            public IEnumerable<TodoModel> TodosMember()
            {
                var userId = User.Claims.ToList()[0].Value;
                return AllTodos.Where(x => x.Members.Any(m => m.Id == userId)).ToList();
            }

            [HttpGet("GetTodo/{id}")]
            public ActionResult GetTodosById(int id)
            {
                var userId = User.Claims.ToList()[0].Value;
                var Todo = AllTodos.FirstOrDefault(i => i.Id == id && i.Author.Id == userId || i.Members.Any(i => i.Id == userId));
                if (Todo != null)
                    return Ok(Todo);

                return Ok(null);
            }

            [HttpGet("Tags/{id}")]
            public IEnumerable<Tags> GetTagsById(int id)
            {
                var userId = User.Claims.ToList()[0].Value;
                var list = AllTodos.FirstOrDefault(i => i.Id == id);
                if (list != null)
                {
                    var tags = AllTags.Where(t => t.TodoId == list.Id).ToList();
                    return tags;
                }

                return null;
            }

            [HttpPost("NewTodo")]
            public IActionResult PostANewTodo(TodoModel model)
            {
                var userId = User.Claims.ToList()[0].Value;
                model.Author = _context.Users.FirstOrDefault(i => i.Id == userId);
                _context.TodoItems.Add(model);

                if (_context.SaveChanges() > 0)
                    return Ok(true);

                return Ok(false);
            }

            [HttpPost("NewTag")]
            public IActionResult PostANewTagToTodo(Tags model)
            {
                var userId = User.Claims.ToList()[0].Value;
                var todo = AllTodos.FirstOrDefault(i => i.Id == model.TodoId && i.Author.Id == userId);
                if (todo != null)
                {
                    _context.TagsItems.Add(model);
                    if (_context.SaveChanges() > 0)
                        return Ok(true);
                }

                return Ok(false);
            }

            [HttpPut("UpdateTodo/{id}")]
            public ActionResult UpdateTodoListWithId(int id, TodoModel model)
            {
                var Todo = AllTodos.FirstOrDefault(i => i.Id == id);
                var userId = User.Claims.ToList()[0].Value;
                if (Todo != null && Todo.Author.Id == userId)
                {
                    Todo.Name = model.Name;
                    Todo.Description = model.Description;
                    if (_context.SaveChanges() > 0)
                        return Ok(true);
                }

                return Ok(false);
            }

            [HttpPut("Todo/{id}/Member/{user_id}")]
            public ActionResult AddNewMemberToList(int id, string user_id)
            {

                var Todo = AllTodos.FirstOrDefault(x => x.Id == id);
                var userId = User.Claims.ToList()[0].Value;
                if (Todo != null && Todo.Author.Id == userId)
                {
                    var member = _context.Users.FirstOrDefault(x => x.Id == user_id);
                    if (member != null)
                    {
                        Todo.Members.Add(member);
                        if (_context.SaveChanges() > 0)
                            return Ok(true);
                    }
                }
                return Ok(false);
            }

            [HttpPut("TodoDone/{todoId}")]
            public ActionResult TodoIsCompleted(int todoId)
            {
                var userId = User.Claims.ToList()[0].Value;
                var Todo = AllTodos.FirstOrDefault(i => i.Id == todoId && i.Author.Id == userId);
                if (Todo != null)
                {
                    Todo.IsComplete = true;
                    if (_context.SaveChanges() > 0)
                        return Ok(true);
                }

                return Ok(false);
            }

        [HttpDelete("DeleteTodo/{id}")]
        public ActionResult DeleteTodo(int id)
        {
            var userId = User.Claims.ToList()[0].Value;
            var todo = AllTodos.FirstOrDefault(x => x.Id == id && x.Author.Id == userId);
            if (todo != null)
            {
                var tags = AllTags.Where(x => x.TodoId == todo.Id).ToList();
                if (tags.Count > 0)
                {
                    for (var i = 0; i < tags.Count; i++)
                        _context.TagsItems.Remove(tags[i]);
                }
                _context.TodoItems.Remove(todo);
                if (_context.SaveChanges() > 0)
                    return Ok(true);
            }
            return Ok(false);
        }

            [HttpGet("SearchTodos/{key}")]
            public IEnumerable<TodoModel> Search(string key)
            {

                List<TodoModel> result = new List<TodoModel>();
                var userId = User.Claims.ToList()[0].Value;
                key = key.ToLower();
                var list = AllTodos.Where(a => a.Author.Id == userId || a.Members.Any(m => m.Id == userId)).ToList();
                if (list.Count > 0)
                {
                    List<Tags> tagsList = new List<Tags>();
                    if (AllTags.Count() > 0 && list.Count > 0)
                    {
                        for (var i = 0; i < list.Count; i++)
                        {
                            var TodoId = list[i].Id;
                            var tag = AllTags.FirstOrDefault(x => x.TodoId == TodoId);
                            if (tag != null)
                                tagsList.Add(tag);
                        }
                    }
                    var foundTodos = list.Where(x => x.Name.ToLower().Contains(key) || x.Description.ToLower().Contains(key)).ToList();
                    for (var i = 0; i < foundTodos.Count; i++)
                        result.Add(foundTodos[i]);
                    if (tagsList.Count() > 0)
                    {
                        var foundTags = tagsList.Where(x => x.Text.ToLower().Contains(key)).ToList();
                        for (var i = 0; i < foundTags.Count; i++)
                        {
                            var id = foundTags[i].TodoId;
                            if (result.Find(x => x.Id == id).Equals(null))
                                result.Add(list.FirstOrDefault(x => x.Id == id));
                        }
                    }
                }
                return result;
            }
    }
}
