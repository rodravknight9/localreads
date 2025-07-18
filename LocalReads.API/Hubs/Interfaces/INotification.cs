using LocalReads.Shared.DataTransfer.Notifications;

namespace LocalReads.API.Hubs.Interfaces;

public interface INotification
{
    Task ReceiveNotification(Notification notification);
}
