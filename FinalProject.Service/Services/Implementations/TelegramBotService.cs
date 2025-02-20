using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FinalProject.Service.Services.Interfaces;

namespace FinalProject.Service.Services.Implementations
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly IConfiguration _configuration;
        private static string _connectionString;

        public TelegramBotService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task StartBotAsync(IConfiguration configuration)
        {
            string botToken = configuration["TelegramBotToken"] ?? throw new ArgumentNullException("Bot token is missing");
            _connectionString = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException("Connection string is missing");

            var botClient = new TelegramBotClient(botToken);
            using var cts = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new() { AllowedUpdates = Array.Empty<UpdateType>() };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandlePollingErrorAsync,
                receiverOptions,
                cts.Token);

            var me = await botClient.GetMeAsync();
            Console.WriteLine($"Bot started: @{me.Username}");
            await Task.Delay(Timeout.Infinite, cts.Token);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message || string.IsNullOrEmpty(message.Text)) return;
            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{message.Text}' message in chat {chatId}.");

            await botClient.SendTextMessageAsync(chatId, $"You said: {message.Text}", cancellationToken: cancellationToken);
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}] {apiRequestException.Message}",
                _ => exception.ToString()
            });
            return Task.CompletedTask;
        }
    }
}
