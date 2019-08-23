namespace ChatworkApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        // TODO POST / PUT メソッドを追加する。

        public async Task<string> PostAsync(string path, params (string key, object value)[] parameters)
        {
            // TODO PUT と POST だけ切り替えられるようにする。
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseUri}{path}")
                                 {
                                     Content = new FormUrlEncodedContent(ConvertToParameters(parameters))
                                 };

            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode) throw new ApiRequestException(response.StatusCode, response.ReasonPhrase);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        internal static IEnumerable<KeyValuePair<string, string>> ConvertToParameters(params (string key, object value)[] parameters)
            => parameters.Where(x => x.value != null)
                         .Select(x => new KeyValuePair<string, string>(x.key, ConvertToValue(x.value)));

        internal static string ConvertToValue(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value is IEnumerable<int> intArray) return string.Join(",", intArray);

            // TODO DateTime? への変換機能を実装する。

            var flag = value as bool?;
            if (flag.HasValue) return flag.Value ? "1" : "0";

            return value.ToString();
        }
    }
}