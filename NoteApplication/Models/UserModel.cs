using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.ComponentModel.DataAnnotations;

namespace NoteApplication.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="Lütfen bir Email giriniz")]
        [EmailAddress(ErrorMessage ="Doğru bir email giriniz")]
        public string? Id { get; set; }
        [StringLength(50,ErrorMessage ="50 uzunlaktan fazla girmeyiniz")]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public List<NoteModel> Notes { get; set; } = [];

    }
}
