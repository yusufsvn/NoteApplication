using Microsoft.AspNetCore.Mvc;
using NoteApplication.Models;

namespace NoteApplication.Controllers
{
    public class MainPageController(LoginController login) : Controller
    {

        private readonly LoginController _loginController = login;

        [HttpGet]
        public IActionResult MainiPage()
        {
            List<NoteModel> notemodels = _loginController.GetNotesAsync();
            //ViewBag["notelist"] = model;
            return View(notemodels);
        }

        [HttpPost]
        public IActionResult GoUpdateNote(string NoteId,string NoteContent)
        {
            int NoteIdNumber = int.Parse(NoteId);
            //var updatedResponse = 
            _loginController.UpdateNote(NoteIdNumber, NoteContent);

            return Ok();
        }


        [Route("/MainPage/GoAddNote")]
        [HttpPost]
        public async Task<IActionResult> GoAddNoteAsync(string Title,string NoteContent)
        {
            bool AddingControl = await _loginController.AddNoteAsync(Title, NoteContent);
            if (AddingControl)
            {
                return Content("Not Başarıyla eklendi");
            }
            return Content("bir hata oluştu");
        }


    }
}
