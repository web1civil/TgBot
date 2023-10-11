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

                if (callbackQuery.Data == "411")
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {

                        var user = await db.Users.FirstOrDefaultAsync(x => x.TgChatId == ChadId);
                        var note = db.Notes.AsNoTracking().FirstOrDefault();

                        var users = db.Users.ToList();
                        Console.WriteLine("Users list:");
                        foreach (User u in users)
                        {
                            Console.WriteLine($"{u.Id}.{u.Name} - ");
                        }
                        int stepCreateNote = note.StageCreate;

                        if (user is null)
                        {
                            User user1 = new User { TgChatId = ChadId, Name = "123" };
                            db.Users.Add(user1);
                            db.SaveChanges();
                        }

                        if (1 == 1)
                        {
                            db.Notes.Max(x => x.Id);
                            Note newNote = new Note { };

                        }


                    }
                }
                if (callbackQuery.Data == "42")
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        User user1 = new User { Name = "Tom", TgChatId = ChadId };
                        db.Users.AddRange(user1);
                        db.SaveChanges();

                        var users = db.Users.ToList();

                        foreach (User u in users)
                        {
                            await botClient.SendTextMessageAsync(
                            chatId: callbackQuery.Message.Chat.Id,
                            text: $"{u.Id}.{u.Name}  ");
                        }
                    }
                }

                if (callbackQuery.Data == "11")
                {
                    var stic = await botClient.SendStickerAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        sticker: InputFile.FromUri("https://tlgrm.ru/_/stickers/20a/2eb/20a2eb19-d51b-4e28-bd0f-4470576af429/10.webp")
                        );
                }
                if (callbackQuery.Data == "12")
                {
                    var stic = await botClient.SendStickerAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        sticker: InputFile.FromUri("https://cdn.tlgrm.app/stickers/dc7/a36/dc7a3659-1457-4506-9294-0d28f529bb0a/192/1.webp")
                        );
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
                if (callbackQuery.Data == "22")
                {
                    await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Люди не болейте");
                }
                if (callbackQuery.Data == "23")
                {
                    await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "негры негры негры");
                }
                if (callbackQuery.Data == "31")
                {
                    await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "https://www.youtube.com/watch?v=Go9i747MzjQ&ab_channel=L1TNEYY");
                }
                if (callbackQuery.Data == "32")
                {
                    await botClient.SendTextMessageAsync(
           chatId: callbackQuery.Message.Chat.Id,
           text: "https://www.youtube.com/watch?v=m_PSmIlNso8&ab_channel=A%24APRocky-Topic");
                }
                if (callbackQuery.Data == "33")
                {
                    await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "https://www.youtube.com/watch?v=dfWt38SJ5c0&ab_channel=RocketFamily");
                }
            }


            if (update.Type == UpdateType.Message)
            {

                var messagetext1 = update.Message;
                var messagetext12 = update.CallbackQuery;


                // Only process Message updates: https://core.telegram.org/bots/api#message
                if (messagetext1 is not { } message)
                    return;
                // Only process text messages
                if (messagetext1 is not { } messageText)
                    return;

                var chatId = message.Chat.Id;

                Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

                // Echo received message text
                // using Telegram.Bot.Types.ReplyMarkups;

                InlineKeyboardMarkup inlineKeyboard = new(new[]
                     {
            // first row
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "СТИКЕР 1", callbackData: "41"),
                InlineKeyboardButton.WithCallbackData(text: "СТИКЕР 2", callbackData: "42"),
                InlineKeyboardButton.WithCallbackData(text: "СТИКЕР 3", callbackData: "13")
            },
            // second row
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