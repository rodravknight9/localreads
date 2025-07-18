using LocalReads.API.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace LocalReads.API.Hubs { 

    public class NotificationHub : Hub<INotification>
    {

        //public async Task SendNotification(string message)
        //{
        //    await Clients.All.SendAsync("ReceiveNotification", message);
        //}

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }

}