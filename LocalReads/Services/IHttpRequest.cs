using LocalReads.Models;
using LocalReads.Shared.DataTransfer;

namespace LocalReads.Services;

public interface IHttpRequest
{
    public Task<HttpResponse<T>> Post<T, Y>(Y entity, string path);
    public Task Post<T>(T entity, string path);
    Task<SimpleHttpResponse> SimplePost<T>(T entity, string path);

    Task<HttpResponse<T>> Get<T>(string path);

    Task<SimpleHttpResponse> Delete(string path);
    Task<SimpleHttpResponse> SimplePut<T>(T entity, string path);
    Task<SimpleHttpResponse> SimplePatch<T>(T entity, string path);



    //New methods for LocalReads responses
    public Task<HttpLocalReadsResponse<TResponse>> SendPost<TRequest, TResponse>(TRequest entity, string path);
}
