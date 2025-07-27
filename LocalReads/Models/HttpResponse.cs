using System.Net;

namespace LocalReads.Models;

public class HttpResponse<T>
{
    public T Content { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public string CodeResponse { get; set; }
}