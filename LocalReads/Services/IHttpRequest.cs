using LocalReads.Models;
using LocalReads.Shared.DataTransfer;

namespace LocalReads.Services;

public interface IHttpRequest
{
    public Task<HttpResponse<T>> Post<T, Y>(Y entity, string path);
    public Task Post<T>(T entity, string path);
    Task<SimpleHttpResponse> SimplePost<T>(T entity, string path);
}
