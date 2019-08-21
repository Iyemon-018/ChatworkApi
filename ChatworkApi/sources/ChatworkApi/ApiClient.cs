namespace ChatworkApi
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal sealed class ApiClient
    {
        private static readonly string BaseUri = "https://api.chatwork.com/v2";

        private static readonly string ApiTokenHeaderKey = "X-ChatWorkToken";

        private readonly string _apiToken;

        private readonly HttpClient _httpClient;

        public ApiClient(string apiToken)
        {
            _apiToken   = apiToken;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAsync(string path, params (string key, string value)[] parameters)
        {
            var requestUri     = $"{BaseUri}{path}{string.Join("?", parameters.Select(x => $"{x.key}={x.value}"))}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Add(ApiTokenHeaderKey, _apiToken);

            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode) throw new NotImplementedException();

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return content;
        }

        public async Task PostAsync(string path, params (string key, string value)[] parameters)
        {
            var requestUri = $"{BaseUri}{path}{string.Join("?", parameters.Select(x => $"{x.key}={x.value}"))}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
            requestMessage.Headers.Add(ApiTokenHeaderKey, _apiToken);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode) throw new NotImplementedException();
        }
    }
}