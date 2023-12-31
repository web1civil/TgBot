﻿using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using CETmsgr.dbutils;
using Update = Telegram.Bot.Types.Update;
using CETmsgr.MaintMethods;

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
                long сhatId = callbackQuery.Message.Chat.Id;
                DataBaseMethods dataBaseMethods = new DataBaseMethods();
                MainProgramMethods mainProgramMethods = new MainProgramMethods();
                dataBaseMethods.AddOrCheckUser(сhatId);
                if (callbackQuery.Data.StartsWith("SelectedNoteMenu"))
                {
                    string idPart = callbackQuery.Data.Substring("SelectedNoteMenu".Length);
                    int noteId;
                    if (int.TryParse(idPart, out noteId))
                    {
                        mainProgramMethods.SendSelectedNoteMenu(botClient, noteId, сhatId);
                    }
                }
                if (callbackQuery.Data.StartsWith("SelectedNoteDelete"))
                {
                    string idPart = callbackQuery.Data.Substring("SelectedNoteDelete".Length);
                    int noteId;
                    if (int.TryParse(idPart, out noteId))
                    {
                        dataBaseMethods.DeleteNote(noteId);
                    }
                }
                if (callbackQuery.Data == "SelectedNoteGoBack")
                    mainProgramMethods.SendInlineNoteButtons(botClient, сhatId);
                if (callbackQuery.Data == "CreateEmptyNewNote")
                    mainProgramMethods.CreateEmptyNewNoteMainProgram(botClient, сhatId);
            }
            if (update.Type != UpdateType.CallbackQuery)
            {
                var messagetext = update.Message;
                if (messagetext is not { } message)
                    return;
                if (messagetext is not { } messageText)
                    return;
                MainProgramMethods mainProgramMethods = new MainProgramMethods();
                DataBaseMethods dataBaseMethods = new DataBaseMethods();
                long chatId = update.Message.Chat.Id;

               if (dataBaseMethods.GetFirstNoteWithStageCreate(chatId) != null)
                     dataBaseMethods.AddTextToNewNote(dataBaseMethods.GetFirstNoteWithStageCreate(chatId).Id, messagetext.Text);
                
                if (messageText.Text == "/start")
                    mainProgramMethods.SendInlineNoteButtons(botClient,chatId);
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