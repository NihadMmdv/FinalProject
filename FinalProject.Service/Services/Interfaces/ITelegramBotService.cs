using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FinalProject.Service.Services.Interfaces
{
    public interface ITelegramBotService
    {
        Task StartBotAsync(IConfiguration configuration);
    }
}
