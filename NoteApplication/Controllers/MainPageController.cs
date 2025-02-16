using Microsoft.AspNetCore.Mvc;
using NoteApplication.Models;
using System.Diagnostics;

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
        public IActionResult GoUpdateNote(string NoteId,string NoteContent,string Title)
        {
            int NoteIdNumber = int.Parse(NoteId);
            //var updatedResponse = 
            _loginController.UpdateNote(NoteIdNumber, NoteContent,Title);

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

        [HttpDelete]
        public async Task<IActionResult> GoDelNoteAsync(string id)
        {
            int newId = Int32.Parse(id);
            Debug.WriteLine(id);
            bool DelState = await _loginController.DelNoteAsync(newId);
            if (DelState)
            {
                return Ok(new { message = "Not başarıyla silindi" });
            }
            else
            {
                return NotFound(new { message = "Not bulunamadı" });
            }
        }
    }
}
