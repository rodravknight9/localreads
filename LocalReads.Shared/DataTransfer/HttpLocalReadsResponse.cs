namespace LocalReads.Shared.DataTransfer;

public class HttpLocalReadsResponse<T>
{
    public string ServerMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; } = false;
    public bool HasServerFeedback { get; set; } = false;
    public string Code { get; set; } = string.Empty;
    public T? Data { get; set; } = default!;
}
