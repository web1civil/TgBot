using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using CETmsgr.dbutils;
namespace CETmsgr.MaintMethods
{
    internal class MainProgramMethods
    {
        //думаю тут много неправильно, хз как с кейбоярдами работать и плохие наименования
        public static void SendInlineNoteButtons (ITelegramBotClient botClient, int chatId)
        {
            var buttons = new List<List<InlineKeyboardButton>>();
            var notes = DataBaseMethods.GetAllIdNotesByOwnerIdToList(chatId);
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
        public static void SendSelectedNoteMenu (ITelegramBotClient botClient, int id, int chatId)
        {
            var buttons = new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData(text: "Назад",     "SelectedNoteMenuGoBack"),
            InlineKeyboardButton.WithCallbackData(text: "Добавить", $"SelectedNoteMenuAddNew{id}"),
            InlineKeyboardButton.WithCallbackData(text: "Изменить", $"SelectedNoteMenuChange{id}"),
            InlineKeyboardButton.WithCallbackData(text: "Удалить",  $"SelectedNoteMenuDelete{id}")
        }
            };
            var keyboard = new InlineKeyboardMarkup(buttons);
            botClient.SendTextMessageAsync(chatId, DataBaseMethods.GetNoteTextByNoteId(id), replyMarkup: keyboard);
        }
        public static void CreateEmptyNewNoteMainProgram (ITelegramBotClient botClient, int chatId)
        {
            DataBaseMethods.CreateEmptyNewNote(chatId);
                botClient.SendTextMessageAsync (
                    chatId: chatId,
                    text: "Введите текст заметки:");
        }
       
    }
}
