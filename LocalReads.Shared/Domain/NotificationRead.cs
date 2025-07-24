namespace LocalReads.Shared.Domain;

public class NotificationRead
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LogActionId  { get; set; }
    public bool  AlreadyRead { get; set; }
    public DateTime ReadOn { get; set; }
    public User User { get; set; }
    public LogAction LogAction { get; set; }
}