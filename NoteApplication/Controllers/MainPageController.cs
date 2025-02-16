using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApplication.Models;
using System.Diagnostics;

namespace NoteApplication.Controllers
{
    public class MainPageController(LoginController login, IHttpContextAccessor httpContextAccessor) : Controller
    {

        private readonly LoginController _loginController = login;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        [HttpGet]
        public IActionResult MainiPage()
        {
            List<NoteModel> notemodels = _loginController.GetNotesAsync();
            return View(notemodels);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> GoUpdateNoteAsync(string NoteId,string NoteContent,string Title)
        {
            int NoteIdNumber = int.Parse(NoteId);
            bool check  = await _loginController.UpdateNoteAsync(NoteIdNumber, NoteContent,Title);
            if (check)
            {
                return RedirectToAction("MainiPage","MainPage");
            }
            return Content("hata oluştu");
            
        }


        [Route("/MainPage/GoAddNote")]
        [HttpPost]
        public async Task<IActionResult> GoAddNoteAsync(string Title,string NoteContent)
        {
            bool AddingControl = await _loginController.AddNoteAsync(Title, NoteContent);
            if (AddingControl)
            {
                return RedirectToAction("MainiPage","MainPage");
            }
            return Content("bir hata oluştu");
        }

        [HttpDelete]
        public async Task<IActionResult> GoDelNoteAsync(string id)
        {
            int newId = Int32.Parse(id);
            Debug.WriteLine(id);
            bool DelState = await _loginController.DelNoteAsync(newId);
            if (DelState)
            {
                return Json(new { redirectUrl = "/MainPage/MainiPage" }); //RedirectToAction("MainiPage", "MainPage");
            }
            else
            {
                return Content("bir hata oluştu");
            }
        }
    }
}
