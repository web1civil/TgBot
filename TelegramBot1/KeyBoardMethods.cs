using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bots.Http;

namespace CETmsgr.keyboards
{
    public static class KeyBoardMethods
    {
        public static InlineKeyboardMarkup inlineKeyboard = new(new[]
             {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "создать заметку", callbackData: "4121"),
                InlineKeyboardButton.WithCallbackData(text: "СТИКЕР 2", callbackData: "2"),
                InlineKeyboardButton.WithCallbackData(text: "СТИКЕР 3", callbackData: "13")
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "АНЕКДОТ 1", callbackData: "2"),
                InlineKeyboardButton.WithCallbackData(text: "АНЕКДОТ 2", callbackData: "22"),
                InlineKeyboardButton.WithCallbackData(text: "АНЕКДОТ 3", callbackData: "23")
            },
             new []
            {
                InlineKeyboardButton.WithCallbackData(text: "ТРЕК 1", callbackData: "31"),
                InlineKeyboardButton.WithCallbackData(text: "получить", callbackData: "112"),
                InlineKeyboardButton.WithCallbackData(text: "ТРЕК 3", callbackData: "33")
            }
        });
        
    }
}
