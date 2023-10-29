using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Configuration;

namespace CETmsgr.dbutils
{
    internal class DataBaseMethods
    {
        public static Note GetNote (int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = db.Notes.Where(u => u.UserId == id).FirstOrDefault();
                return Note;
            }
        }
        public static string  GetNoteTextByNoteId (int id)
        { 
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = GetNote(id);
                if (Note != null)
                    return Note.Text;
                else
                    return "текста нет" ;
            }
        }
        public static async Task SetChangeTextNote (int id,string newText)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = GetNote(id);
                Note.Text = newText;
                db.Notes.Update(Note);
                db.SaveChanges();
            }
        }
        public static async Task AddTextToNote (int id, string text)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = GetNote(id);
                Note.Text += text;
                db.Notes.Update(Note);
                db.SaveChanges();
            }
        }
        public static async Task DeleteNote (int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = GetNote(id);
                db.Notes.Remove(Note);
                db.SaveChanges();
            }
        }
        public static async Task AddOrCheckUser (long id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user == null)
                {
                    user = new User { Id = id };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }
        public static List<int> GetAllIdNotes (long id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<int> NotesId = new List<int>();
                foreach (var u in AllNotes(id))
                    NotesId.Add(u.Id);
                return NotesId;
            }
        }
        public static Note GetFirstNoteWithStageCreate(long id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (AllNotes(id).Count == 0)
                    return null;
                return AllNotes(id).FirstOrDefault(n => n.StageCreate == 1);
            }
        }
        public static List<Note> AllNotes (long id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Notes.Where(n => n.UserId == id).ToList();
            }
        }
        public static async Task CreateEmptyNewNote(long id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Note NewNote1 = new Note { UserId = id, StageCreate = 1 };
                db.Notes.Add(NewNote1);
                db.SaveChanges();
            }
        }
        public static async Task AddTextToNewNote(int id, string noteText)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = db.Notes.FirstOrDefault(n => n.Id == id);
                Note.Text = noteText;
                Note.StageCreate = 0;
                db.Notes.Update(Note);
                db.SaveChanges();
            }
        }
    }
}

