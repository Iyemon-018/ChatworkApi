namespace ChatworkApi
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal sealed class ApiClient
    {
        private static readonly string BaseUri = "https://api.chatwork.com/v2";

        private readonly HttpClient _httpClient;

        public ApiClient(string apiToken)
        {
            _httpClient = new HttpClient(new AuthenticationHandler(apiToken));
        }

        public async Task<string> GetAsync(string path, params (string key, object value)[] parameters)
        {
            var requestUri     = RequestUriGenerator.Generate(BaseUri, path, parameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode) throw new ApiRequestException(response.StatusCode, response.ReasonPhrase);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return content;
        }

        public async Task PostAsync(string path, params (string key, object value)[] parameters)
        {
            var requestUri     = RequestUriGenerator.Generate(BaseUri, path, parameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode) throw new ApiRequestException(response.StatusCode, response.ReasonPhrase);
        }
    }
}