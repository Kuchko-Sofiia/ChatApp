namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IWrappedHttpClient
    {
        public Task<HttpResponseMessage> GetAsync(string url);
    }
}
