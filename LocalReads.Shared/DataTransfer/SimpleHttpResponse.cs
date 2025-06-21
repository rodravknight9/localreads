using System.Net;

namespace LocalReads.Shared.DataTransfer;

public class SimpleHttpResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}
