using LocalReads.API.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace LocalReads.API.Hubs;  

public class NotificationHub : Hub<INotification>
{
}