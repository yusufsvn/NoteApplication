using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApplication.Models;
using System.Diagnostics;
using System.Linq;
namespace NoteApplication.Controllers
{
    [NonController]
    public class LoginController(IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly NoteContext context = new();

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [NonAction]
        public async Task<bool> LoginUser(string email, string password)
        {
            List<UserModel> Users = await context.Users.ToListAsync();

            int i = 0;
            while (i < Users.Count)
            {

                if (Users[i].Id.Equals(email, StringComparison.Ordinal) && Users[i].Password.Equals(password, StringComparison.Ordinal))
                {

                    _httpContextAccessor.HttpContext?.Session.SetString("UserId", Users[i].Id);
                    return true;
                }
                i++;
            }
            return false;
        }

        [NonAction]
        public async Task AddUserAsync(UserModel user)
        {
            if (ModelState.IsValid)
            {
                //Burada hata denetimi eksiği var : aynı maille kayıt olmamalı primary key zaten bu olayı çözüyor ancak kullanıcya bildirecek mekanizma yok
                //addProcesses
                await context.AddAsync(user);
                await context.SaveChangesAsync();  
            }
        }
        
        [NonAction]
        public async Task<bool> AddNoteAsync(string Title, string Content)
        {
            var UserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId"); // null geliyor
            if (UserId != null) {
                var dt = DateTime.Now.ToString();

                var Note = new NoteModel
                {
                    NoteContent = Content,
                    Title = Title,
                    LastChanging = dt,
                    UserId = UserId
                };
                if(Note != null)
                {
                    await context.AddAsync(Note);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }            
            return  false;
        }

        [NonAction]
        public List<NoteModel> GetNotesAsync()
        {
            string? UserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            List<NoteModel> Notes = (from note in context.Notes
                         where note.UserId == UserId
                                     select note).ToList();

            return Notes;
        }
        
        [NonAction]
        public void UpdateNote(int NoteId, string NoteContent,string Title)
        {
            string? UserId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            /*
            Debug.WriteLine(UserId);
            Debug.WriteLine(NoteId);
            Debug.WriteLine(NoteContent);
            */
            NoteModel? obj = (from note in context.Notes where note.Id == NoteId && note.UserId == UserId select note).FirstOrDefault();
            if(obj != null)
            {
                var dt = DateTime.Now.ToString();
                obj.NoteContent = NoteContent;
                obj.LastChanging = dt;
                obj.Title = Title;
                context.SaveChanges();
            }
            else
            {
                Debug.WriteLine("obj null");
            }
        }

        [NonAction]
        public async Task<bool> DelNoteAsync(int id)
        {
            Debug.WriteLine(id);
            var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if(note != null)
            {
                context.Remove(note);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
            
        }

    }
}
