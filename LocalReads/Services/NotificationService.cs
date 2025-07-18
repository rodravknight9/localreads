using LocalReads.Shared.DataTransfer.Notifications;
using Microsoft.AspNetCore.SignalR.Client;

namespace LocalReads.Services;

public class NotificationService
{
    public Queue<Notification> Notifications = new Queue<Notification>();
    public event Action? OnNotificationReceived;
    public int NotificationCount { get; private set; }
    private HubConnection? _hubConnection;

    public async Task StartAsync()
    {
        try
        {
            await Task.Delay(100);

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7223/notification-hub")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<Notification>("ReceiveNotification", (notification) =>
            {
                Notifications.Enqueue(notification);
                NotificationCount++;
                OnNotificationReceived?.Invoke();
            });

            await _hubConnection.StartAsync();
        }
        catch (Exception e)
        {

            throw;
        }
       
    }
}
