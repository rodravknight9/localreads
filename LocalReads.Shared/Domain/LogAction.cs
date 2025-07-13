namespace LocalReads.Shared.Domain;

public class LogAction
{
    public int Id { get; set; }
    public string Action { get; set; }
    public string Table  { get; set; }
    public string RecordId { get; set; }
    public DateTime ActionTime { get; set; }
    public int UserId { get; set; }
}