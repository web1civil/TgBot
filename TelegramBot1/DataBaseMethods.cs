using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Configuration;

namespace CETmsgr.dbutils
{
    internal class DataBaseMethods
    {
        ApplicationContext db = new ApplicationContext();
        public Note GetNote(int noteId)
        {
            return db.Notes.Where(u => u.Id == noteId).FirstOrDefault();
        }
        public string GetNoteTextByNoteId(int noteId)
        {
            var note = GetNote(noteId);     
            return (note != null) ? note.Text : "текста нет";
        }
        public void SetChangeTextNote(int noteId, string newText)
        {
            var note = GetNote(noteId);
            note.Text = newText;
            db.Notes.Update(note);
            db.SaveChanges();
        }
        public void AddTextToNote(int noteId, string text)
        {
            var note = GetNote(noteId);
            note.Text += text;
            db.Notes.Update(note);
            db.SaveChanges();
        }
        public void DeleteNote(int noteId)
        {
            var note = GetNote(noteId);
            db.Notes.Remove(note);
            db.SaveChanges();
        }
        public void AddOrCheckUser(long userId)
        {
            var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                user = new User { Id = userId };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public List<int> GetAllIdNotes(long userId)
        {
            List<int> notesId = new List<int>();
            foreach (var u in AllNotes(userId))
                notesId.Add(u.Id);
            return notesId;
        }
        public Note GetFirstNoteWithStageCreate(long userId)
        {
            if (AllNotes(userId).Count == 0)
                return null;
            return AllNotes(userId).FirstOrDefault(n => n.StageCreate == 1);
        }
        public List<Note> AllNotes(long userId)
        {
            return db.Notes.Where(n => n.UserId == userId).ToList();
        }
        public void CreateEmptyNewNote(long userId)
        {
            Note newNote = new Note { UserId = userId, StageCreate = 1 };
            db.Notes.Add(newNote);
            db.SaveChanges();
        }
        public void AddTextToNewNote(int noteId, string noteText)
        {
            var note = db.Notes.FirstOrDefault(n => n.Id == noteId);
            note.Text = noteText;
            note.StageCreate = 0;
            db.Notes.Update(note);
            db.SaveChanges();
        }
    }
}

