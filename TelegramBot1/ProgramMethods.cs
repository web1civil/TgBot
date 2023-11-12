using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using CETmsgr.dbutils;
namespace CETmsgr.MaintMethods
{
    internal class MainProgramMethods
    {

        DataBaseMethods dataBaseMethods = new DataBaseMethods();
        //думаю тут много неправильно, хз как с кейбоярдами работать и плохие наименования
        public void SendInlineNoteButtons (ITelegramBotClient botClient, long chatId)
        {
            var buttons = new List<List<InlineKeyboardButton>>();

            var notes = dataBaseMethods.GetAllIdNotes(chatId);
            foreach (var note in notes)
            {
                buttons.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(dataBaseMethods.GetNoteTextByNoteId(note), $"SelectedNoteMenu{note}")
                });
            }
            buttons.Add(new List<InlineKeyboardButton>{InlineKeyboardButton.WithCallbackData("Cоздать новую заметку", "CreateEmptyNewNote")});
            var keyboard = new InlineKeyboardMarkup(buttons);
            botClient.SendTextMessageAsync(chatId, "Выберите заметку:", replyMarkup: keyboard);
        }
        public void SendSelectedNoteMenu (ITelegramBotClient botClient, int id, long chatId)
        {
            var buttons = new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Назад",     "SelectedNoteGoBack"),
                    InlineKeyboardButton.WithCallbackData(text: "Добавить", $"SelectedNoteAddNew{id}"),
                    InlineKeyboardButton.WithCallbackData(text: "Изменить", $"SelectedNoteChange{id}"),
                    InlineKeyboardButton.WithCallbackData(text: "Удалить",  $"SelectedNoteDelete{id}")
                }
            };
            var keyboard = new InlineKeyboardMarkup(buttons);
            botClient.SendTextMessageAsync(chatId, dataBaseMethods.GetNoteTextByNoteId(id), replyMarkup: keyboard);
        }
        public void CreateEmptyNewNoteMainProgram (ITelegramBotClient botClient, long chatId)
        {
            dataBaseMethods.CreateEmptyNewNote(chatId);
            botClient.SendTextMessageAsync (
                chatId: chatId,
                text: "Введите текст заметки:");
        }
       
    }
}
