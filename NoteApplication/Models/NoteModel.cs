namespace NoteApplication.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string? NoteContent { get; set; }
        public string? Title { get; set; }
        public string? LastChanging { get; set; }
        public required string? UserId { get; set; }
        public UserModel? User { get; set; }
    }

}
