using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinalProject.Service.Services.Telegram
{
    public static class TelegramBot
    {
        private static string _connectionString;

        public static async Task StartBotAsync(IConfiguration configuration)
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
            Console.ReadLine();
            cts.Cancel();
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message || string.IsNullOrEmpty(message.Text)) return;
            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{message.Text}' message in chat {chatId}.");

            switch (message.Text.ToLower().Trim())
            {
                case "hello":
                    await SendTextResponse(botClient, chatId, "Hello, I am TurboBid Bot!\nKey commands:\n/address - Get address info\n/auction - View auction cars\n/blog - View blogs", cancellationToken);
                    break;
                case "salam":
                    await SendTextResponse(botClient, chatId, "Salam, mən TurboBid Bot-am!\nAçar sözlər:\n/address - Address haqqında\n/auction - Auksion maşınları\n/blog - Bloglar", cancellationToken);
                    break;
                case "/auction":
                    await SendAuctionInfo(botClient, chatId, cancellationToken);
                    break;
                case "/blog":
                    await SendBlogInfo(botClient, chatId, cancellationToken);
                    break;
                case "/address":
                    await botClient.SendVenueAsync(chatId, 40.3772535f, 49.8540073f, "Man Hanging out", "Azerbaijan, Baku", cancellationToken: cancellationToken);
                    break;
                default:
                    await SendTextResponse(botClient, chatId, "You said:\n" + message.Text, cancellationToken);
                    break;
            }
        }

        private static async Task SendAuctionInfo(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync(cancellationToken);
            string query = "SELECT Id, m.Name, FabricationYear, Price, ActionDate, b.Name, ci.Image " +
                           "FROM Cars c INNER JOIN Models m on c.ModelId = m.Id " +
                           "INNER JOIN Brands b on m.BrandId = b.Id " +
                           "INNER JOIN CarImages ci on ci.CarId = c.Id " +
                           "WHERE (StatusId = 2 AND ci.isMain = 1) OR (StatusId = 1 AND ci.isMain = 1)";

            using var cmd = new SqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                string text = $"Id: {reader["Id"]}\nModel: {reader["Name"]} {reader["FabricationYear"]}\n" +
                              $"Auction Start Price: ${reader["Price"]}\nAuction Date: {reader["ActionDate"]}";

                string imageUrl = "http://yourwebsite.com/Images/Cars/" + reader["Image"];
                await botClient.SendPhotoAsync(chatId, InputFile.FromUri(imageUrl), cancellationToken: cancellationToken);
                await SendTextResponse(botClient, chatId, text, cancellationToken);
            }
        }

        private static async Task SendBlogInfo(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync(cancellationToken);
            string query = "SELECT Id, Title, Author, Image, LEFT(Description, 100) AS ShortDesc FROM Blogs WHERE IsDeleted = 0";

            using var cmd = new SqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                string text = $"Id: {reader["Id"]}\nTitle: {reader["Title"]}\nAuthor: {reader["Author"]}\nText: {reader["ShortDesc"]}...";

                string imageUrl = "http://yourwebsite.com/Images/Blogs/" + reader["Image"];
                await botClient.SendPhotoAsync(chatId, InputFile.FromUri(imageUrl), cancellationToken: cancellationToken);
                await SendTextResponse(botClient, chatId, text, cancellationToken);
            }
        }

        private static async Task SendTextResponse(ITelegramBotClient botClient, long chatId, string text, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId, text, cancellationToken: cancellationToken);
        }

        private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
