using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API
{
    public class NotificationHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("LocalizationChanges", message);
        }
    }
}
