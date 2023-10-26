using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using CETmsgr.dbutils;
namespace CETmsgr.MaintMethods
{
    internal class MainProgramMethods
    {
        //думаю тут много неправильно, хз как с кейбоярдами работать и плохие наименования
        public static void SendInlineNoteButtons (ITelegramBotClient botClient, long chatId)
        {
            var buttons = new List<List<InlineKeyboardButton>>();
            var notes = DataBaseMethods.GetAllIdNotes(chatId);
            foreach (var note in notes)
            {
                buttons.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(DataBaseMethods.GetNoteTextByNoteId(note), $"SelectedNoteMenu{note}")
                });
            }
            buttons.Add(new List<InlineKeyboardButton>{InlineKeyboardButton.WithCallbackData("Cоздать новую заметку", "CreateEmptyNewNote")});
            var keyboard = new InlineKeyboardMarkup(buttons);
            botClient.SendTextMessageAsync(chatId, "Выберите заметку:", replyMarkup: keyboard);
        }
        public static void SendSelectedNoteMenu (ITelegramBotClient botClient, int id, long chatId)
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
            botClient.SendTextMessageAsync(chatId, DataBaseMethods.GetNoteTextByNoteId(id), replyMarkup: keyboard);
        }
        public static void CreateEmptyNewNoteMainProgram (ITelegramBotClient botClient, long chatId)
        {
            DataBaseMethods.CreateEmptyNewNote(chatId);
            botClient.SendTextMessageAsync (
                chatId: chatId,
                text: "Введите текст заметки:");
        }
       
    }
}
