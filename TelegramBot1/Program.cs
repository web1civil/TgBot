using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Polling;
using Microsoft.EntityFrameworkCore;
using CETmsgr.dbutils;
using Update = Telegram.Bot.Types.Update;
using Telegram.Bot.Types.Enums;
namespace CETmsgr
{


    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6609199550:AAGPenZ66Tkrp7ZSBEqCicZsVWQYPZpOtdc");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackQuery = update.CallbackQuery;
                int ChadId = (int)Convert.ToInt64(update.CallbackQuery.Message.Chat.Id);
                await DataBaseMethods.FindOrAddUser(ChadId);
                if (callbackQuery.Data == "41")
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var users = (from user in db.Users.Include(p => p.Id)
                                     where user.Id == ChadId
                                     select user);

                        var users1 = db.Users.ToList();
                        Console.WriteLine("Данные после добавления:");
                        foreach (User u in users)
                        {
                            await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: $"{u.Id}");

                        }



                    }
                }
                if (callbackQuery.Data == "4121")
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        DataBaseMethods.CreateEmptyNewNote(ChadId);
                        await botClient.SendTextMessageAsync(
          chatId: callbackQuery.Message.Chat.Id,
          text: "Введите текст заметки");
                    }
                }

                if (callbackQuery.Data == "13")
                {
                    var stic = await botClient.SendStickerAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        sticker: InputFile.FromUri("https://cdn.tlgrm.app/stickers/696/3ad/6963ad3a-2019-32d2-ac85-34af3c84e50b/192/10.webp")
                        );
                }
                if (callbackQuery.Data == "21")
                {
                    await botClient.SendTextMessageAsync(
             chatId: callbackQuery.Message.Chat.Id,
             text: "Заходит улитка в бар");
                }
             
            }


            if (update.Type != UpdateType.CallbackQuery)
            {
                int ChadId = (int)Convert.ToInt64(update.Message.Chat.Id);
                var messagetext = update.Message;
                if (messagetext is not { } message)
                    return;
                if (messagetext is not { } messageText)
                    return;
                string messagetext1 = (string)Convert.ToString(update.Message.Chat.Id);
                var chatId = message.Chat.Id;
                var Note = DataBaseMethods.GetNoteByTgChatId(ChadId);
                if (Note != null)
                {
                    
                    await DataBaseMethods.AddTextToNewNote(Note.Id, messagetext1);
                }
                Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

                InlineKeyboardMarkup inlineKeyboard = new(new[]
                     {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "создать заметку", callbackData: "4121"),
                InlineKeyboardButton.WithCallbackData(text: "СТИКЕР 2", callbackData: "42"),
                InlineKeyboardButton.WithCallbackData(text: "СТИКЕР 3", callbackData: "13")
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "АНЕКДОТ 1", callbackData: "21"),
                InlineKeyboardButton.WithCallbackData(text: "АНЕКДОТ 2", callbackData: "22"),
                InlineKeyboardButton.WithCallbackData(text: "АНЕКДОТ 3", callbackData: "23")
            },
             new []
            {
                InlineKeyboardButton.WithCallbackData(text: "ТРЕК 1", callbackData: "31"),
                InlineKeyboardButton.WithCallbackData(text: "ТРЕК 2", callbackData: "32"),
                InlineKeyboardButton.WithCallbackData(text: "ТРЕК 3", callbackData: "32")
            }
        });
              await botClient.SendTextMessageAsync(
              chatId: chatId,
              text: "Твои опции",
              replyMarkup: inlineKeyboard
              );
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();
        }
    }
}