using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mvc.Controllers
{
    public class TodosController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public TodosController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [Route("Todos")]
        [Route("Todos/member")]
        public async Task<IActionResult> Todos()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            TheWebApi _api = new TheWebApi();

            var url = "api" + Request.Path.Value;

            List<TodoModel> todos = new List<TodoModel>();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                todos = JsonConvert.DeserializeObject<List<TodoModel>>(result);
            }

            return View(todos);
        }

        [Route("ViewTodo/{id}")]
        public async Task<IActionResult> ViewTodos(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            TheWebApi _api = new TheWebApi();

            var todos = new TodoModel();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.GetAsync("api/Todos/GetTodo/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                todos = JsonConvert.DeserializeObject<TodoModel>(result);
            }

            return View(todos);
        }


        [Route("NewTodo")]
        public ActionResult NewTodo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTodo(TodoModel model)
        {
            TheWebApi _api = new TheWebApi();

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            HttpClient http = _api.Initial();

            var list = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(list);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage res = await http.PostAsync("api/Todos/NewTodo", content);
            if (res.IsSuccessStatusCode)
            {
                return Ok(true);
            }

            return Ok(false);
        }

        [Route("EditTodo/{id}")]
        public async Task<ActionResult> EditTodo(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            TheWebApi _api = new TheWebApi();

            var todos = new TodoModel();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.GetAsync("api/Todos/GetTodo/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                todos = JsonConvert.DeserializeObject<TodoModel>(result);
            }

            return View(todos);
        }

        [HttpPut]
        public async Task<IActionResult> EditTodo(TodoModel model)
        {
            TheWebApi _api = new TheWebApi();

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            HttpClient http = _api.Initial();

            var list = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(list);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage res = await http.PutAsync("api/Todos/UpdateTodo/" + model.Id, content);
            if (res.IsSuccessStatusCode)
            {
                return Ok(true);
            }

            return Ok(false);
        }

        [HttpPut]
        public async Task<IActionResult> DoneTodo(int id)
        {
            TheWebApi _api = new TheWebApi();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.PutAsync("api/Todos/TodoDone/" + id, null);
            if (res.IsSuccessStatusCode)
            {
                return Ok(true);
            }

            return Ok(false);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            TheWebApi _api = new TheWebApi();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.DeleteAsync("api/Todos/DeleteTodo/" + id);
            if (res.IsSuccessStatusCode)
            {
                return Ok(true);
            }

            return Ok(false);
        }

        [Route("InviteUsersToTodo/{todoId}")]
        public async Task<IActionResult> Users(int todoId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            TheWebApi _api = new TheWebApi();
            HttpClient http = _api.Initial();
            List<User> users = new List<User>();
            TodoModel todo = new TodoModel();
            HttpResponseMessage res = await http.GetAsync("api/Users");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(result);
                HttpResponseMessage todoResult = await http.GetAsync("api/Todos/GetTodo/" + todoId);
                if (todoResult.IsSuccessStatusCode)
                {
                    var resultMyTodo = todoResult.Content.ReadAsStringAsync().Result;
                    todo = JsonConvert.DeserializeObject<TodoModel>(resultMyTodo);
                }
            }

            var list = Tuple.Create(users, todo);
            return View(list);
        }

        [HttpPut]
        public async Task<IActionResult> InviteUserToTodo(int id, string userId)
        {
            TheWebApi _api = new TheWebApi();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.PutAsync("api/Todos/Todo/" + id + "/Member/" + userId, null);
            if (res.IsSuccessStatusCode)
            {
                return Ok(true);
            }

            return Ok(false);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserFromTodo(int id, string userId)
        {
            TheWebApi _api = new TheWebApi();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.DeleteAsync("api/Users/DeleteMemberFromTodo/" + id + "/Member/" + userId);
            if (res.IsSuccessStatusCode)
            {
                return Ok(true);
            }

            return Ok(false);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAccount()
        {
            TheWebApi _api = new TheWebApi();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.DeleteAsync("api/Users/DeleteAccount");
            if (res.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            return Ok(false);
        }

        [Route("NewTodoTag/{todoId}")]
        public ActionResult NewTodoTag()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewTodoTag(Tags model)
        {
            TheWebApi _api = new TheWebApi();

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            HttpClient http = _api.Initial();

            var list = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(list);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage res = await http.PostAsync("api/Todos/NewTag", content);
            if (res.IsSuccessStatusCode)
            {
                return Ok(true);
            }

            return Ok(false);
        }

 
        [Route("SearchResult")]
        public async Task<IActionResult> Todos(string key)
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            TheWebApi _api = new TheWebApi();
            List<TodoModel> todos = new List<TodoModel>();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.GetAsync("api/Todos/SearchTodos/" + key);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                todos = JsonConvert.DeserializeObject<List<TodoModel>>(result);
            }

            return View(todos);
        }
    }
}
