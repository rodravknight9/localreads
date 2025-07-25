namespace LocalReads.Shared.DataTransfer.Notifications;

public class Notification
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    public int UserId { get; set; }
}
