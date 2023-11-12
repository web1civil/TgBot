using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Configuration;

namespace CETmsgr.dbutils
{
    internal class DataBaseMethods
    {
        ApplicationContext db = new ApplicationContext();
        public Note GetNote(int id)
        {
            var Note = db.Notes.Where(u => u.UserId == id).FirstOrDefault();
            return Note;
        }
        public string GetNoteTextByNoteId(int id)
        {
            var Note = GetNote(id);
            if (Note != null)
                return Note.Text;
            else
                return "текста нет";
        }
        public async Task SetChangeTextNote(int id, string newText)
        {
            var Note = GetNote(id);
            Note.Text = newText;
            db.Notes.Update(Note);
            db.SaveChanges();
        }
        public async Task AddTextToNote(int id, string text)
        {
            var Note = GetNote(id);
            Note.Text += text;
            db.Notes.Update(Note);
            db.SaveChanges();
        }
        public async Task DeleteNote(int id)
        {
            var Note = GetNote(id);
            db.Notes.Remove(Note);
            db.SaveChanges();
        }
        public async Task AddOrCheckUser(long id)
        {
            var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user == null)
            {
                user = new User { Id = id };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public List<int> GetAllIdNotes(long id)
        {
            List<int> NotesId = new List<int>();
            foreach (var u in AllNotes(id))
                NotesId.Add(u.Id);
            return NotesId;
        }
        public Note GetFirstNoteWithStageCreate(long id)
        {
            if (AllNotes(id).Count == 0)
                return null;
            return AllNotes(id).FirstOrDefault(n => n.StageCreate == 1);
        }
        public List<Note> AllNotes(long id)
        {
            return db.Notes.Where(n => n.UserId == id).ToList();
        }
        public async Task CreateEmptyNewNote(long id)
        {
            Note NewNote1 = new Note { UserId = id, StageCreate = 1 };
            db.Notes.Add(NewNote1);
            db.SaveChanges();
        }
        public async Task AddTextToNewNote(int id, string noteText)
        {
            var Note = db.Notes.FirstOrDefault(n => n.Id == id);
            Note.Text = noteText;
            Note.StageCreate = 0;
            db.Notes.Update(Note);
            db.SaveChanges();
        }
    }
}

