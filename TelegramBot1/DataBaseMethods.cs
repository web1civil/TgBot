
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace CETmsgr.dbutils
{
    internal class DataBaseMethods
{
        // метод для проверки есть ли в базе данных юзер с таким же айди и если нету то создает нового с этим айди
        public static async Task FindOrAddUser(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.Where(u => u.TgChatId == id).FirstOrDefault(); 

                if (user == null)
                {
                    user = new User { TgChatId = id };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }
        public static Note GetNoteByTgChatId(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                    var notes = db.Notes.Where(n => n.OwnerId == Id).ToList();

                    if (notes.Count == 0)
                    {
                        return null;
                    }

                    return notes.FirstOrDefault(n => n.StageCreate >= 1);
                
            }
        }
        public static async Task CreateEmptyNewNote(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Note NewNote1 = new Note{OwnerId = Id, StageCreate = 1};
                db.Notes.Add(NewNote1);
                db.SaveChanges();
            }
        }
        public static async Task AddTextToNewNote(int Id,string NoteText)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = db.Notes.FirstOrDefault(n => n.Id == Id);
                Note.Text = NoteText;
                db.Notes.Update(Note);
                db.SaveChanges();
            }
        }
    }
}