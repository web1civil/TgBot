using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Polling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


var botClient = new TelegramBotClient("6609199550:AAGPenZ66Tkrp7ZSBEqCicZsVWQYPZpOtdc");
using CancellationTokenSource cts = new();

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() 
};
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);
var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
 
cts.Cancel();





async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken )
{

  

    if (update.Type == UpdateType.CallbackQuery)
    {
        var callbackQuery = update.CallbackQuery;
        var ChadId = update.CallbackQuery.Message.Chat.Id;

        if (callbackQuery.Data == "41")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
              
                User user1 = new User { TgChatId = ChadId, Name = "123" };
                db.Users.Add(user1);
                db.SaveChanges();
                
                
                
            }
        }
      
        if (callbackQuery.Data == "42")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                
                var user = await db.Users.FirstOrDefaultAsync(x => x.TgChatId == ChadId);
                var note =  db.Notes.AsNoTracking().FirstOrDefault();

                int stepCreateNote = note.StageCreate;

                if(user is null)
                {
                    User user1 = new User { TgChatId = ChadId, Name = "123" };
                    db.Users.Add(user1);
                    db.SaveChanges();
                }

                if (1==1)
                {
                    db.Notes.Max(x => x.Id);
                    Note newNote = new Note { };
                    
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
     text: "Я полу футболиcт - Пинаю хуи");
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
    text: "Негры пидарасы");
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
    if (update.Type != UpdateType.CallbackQuery)
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

        if (update.Type == UpdateType.CallbackQuery)
        {

            await botClient.SendTextMessageAsync(
     chatId: chatId,
     text: "у");
        }
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


        if (update.Type == UpdateType.CallbackQuery)
        {
            var callbackQuery = update.CallbackQuery;
            await botClient.SendTextMessageAsync(
     chatId: chatId,
     text: "п",
     replyMarkup: inlineKeyboard,
     cancellationToken: cancellationToken);
        }

        if (messagetext1.Text == "Call me ☎️")
        {
            var stic = await botClient.SendStickerAsync(
                chatId: chatId,
                sticker: InputFile.FromUri("https://tlgrm.ru/_/stickers/20a/2eb/20a2eb19-d51b-4e28-bd0f-4470576af429/10.webp")
                );
        }


    }
}





Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}