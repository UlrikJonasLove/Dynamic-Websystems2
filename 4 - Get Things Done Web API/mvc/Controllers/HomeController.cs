using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Helpers;
using mvc.Models;
using System.Diagnostics;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenManager _manager;

        private readonly TheWebApi _api = new TheWebApi();

        public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager, ITokenManager manager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _manager = manager;
        }

        [Route("")]
        public IActionResult Index()
        {
            var token = _manager.GetToken();
            if (token.Length == 0 && User.Identity.IsAuthenticated)
            {
                _signInManager.SignOutAsync();
                return Redirect("/");
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
