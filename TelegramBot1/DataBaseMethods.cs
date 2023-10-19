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
                var Note = db.Notes.Where(u => u.Id == id).FirstOrDefault();
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
                    return null;
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
        public static async Task AddTextToNote (int id, string newText)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = GetNote(id);
                Note.Text += newText;
                db.Notes.Update(Note);
                db.SaveChanges();
            }
        }
        public static async Task DeleteNote (int id, string newText)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var Note = GetNote(id);
                db.Notes.Remove(Note);
                db.SaveChanges();
            }
        }
        public static async Task AddOrCheckUser (int id)
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
        public static List<int> GetAllIdNotesByOwnerIdToList (int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var AllNotes = AllNotesByOwnerId(id);
                List<int> NotesIdByOwnerId = new List<int>();
                foreach (var u in AllNotes)
                    NotesIdByOwnerId.Add(u.Id);
                return NotesIdByOwnerId;
            }
        }
        public static Note GetFirstNoteWithStageCreate(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var AllNotes = AllNotesByOwnerId(id);
                if (AllNotes.Count == 0)
                    return null;
                return AllNotes.FirstOrDefault(n => n.StageCreate == 1);
            }
        }
        public static List<Note> AllNotesByOwnerId (int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var AllNotesByOwnerId = db.Notes.Where(n => n.OwnerId == id).ToList();
                
                return AllNotesByOwnerId;
            }
        }
        public static async Task CreateEmptyNewNote(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Note NewNote1 = new Note { OwnerId = id, StageCreate = 1 };
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

