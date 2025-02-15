using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NoteApplication.Models;

namespace NoteApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginController _loginController;

        public HomeController(LoginController login)
        {
            _loginController = login;
        }
        public IActionResult Index()
        {
            return View();
        }

        public LoginController Get_loginController()
        {
            return _loginController;
        }

        [HttpPost]
        public async Task<IActionResult> Index1Async(string email, string password)
        {
            bool result = await _loginController.LoginUser(email, password);
            if (result)
            {
                
                return RedirectToAction("MainiPage","MainPage");
            }
            return Content("hatalı giriş bilgileri");
            
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(string UserName,string Email,string Password)
        {
            var User = new UserModel
            {
                Id = Email,
                Password = Password,
                UserName = UserName
            };
            await _loginController.AddUserAsync(User);
            return RedirectToAction("MainiPage","MainPage");
        }


    }
}
