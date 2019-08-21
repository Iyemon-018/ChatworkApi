namespace ChatworkApi
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class AuthenticationHandler : DelegatingHandler
    {
        private static readonly string ApiTokenHeaderKey = "X-ChatWorkToken";

        private readonly string _apiToken;

        public AuthenticationHandler(string apiToken) : this(apiToken, new HttpClientHandler())
        {
        }

        /// <inheritdoc />
        public AuthenticationHandler(string apiToken, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _apiToken = apiToken;
        }

        /// <summary>Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.</summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request">request</paramref> was null.</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(ApiTokenHeaderKey, _apiToken);

            return base.SendAsync(request, cancellationToken);
        }
    }
}