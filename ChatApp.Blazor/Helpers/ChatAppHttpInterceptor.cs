using System.Net.Http.Headers;

namespace ChatApp.Blazor.Helpers
{
    public class ChatAppHttpInterceptor : DelegatingHandler
    {
        // Inject IHttpContextAccessor to get access to HttpContext in Blazor
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatAppHttpInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task OnRequestAsync(HttpRequestMessage request)
        {

            request.Headers.Add("Authorization", "Bearer myAccessToken");
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Modify request before it is sent
            request.Headers.Add("Authorization", "Bearer myAccessToken");

            // Call the inner handler to send the request
            var response = await base.SendAsync(request, cancellationToken);

            // Modify response after it is received
            if (!response.IsSuccessStatusCode)
            {
                // Handle error response
            }

            return response;
        }

        public async Task OnResponseAsync(HttpResponseMessage response)
        {
            // Modify response after it is received
            if (!response.IsSuccessStatusCode)
            {
                // Handle error response
            }
        }
    }
}
