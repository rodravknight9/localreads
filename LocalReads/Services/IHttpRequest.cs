namespace LocalReads.Services;

public interface IHttpRequest
{
    public Task<T> Post<T, Y>(Y entity, string path);
    public Task Post<T>(T entity, string path);
}
