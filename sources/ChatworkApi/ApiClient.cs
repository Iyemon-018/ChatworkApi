namespace ChatworkApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Chatwork API を実行するための機能を提供します。
    /// </summary>
    /// <remarks>
    /// http://developer.chatwork.com/ja/endpoints.html
    /// </remarks>
    internal sealed class ApiClient
    {
        /// <summary>
        /// ベース URI
        /// </summary>
        private static readonly string BaseUri = "https://api.chatwork.com/v2";

        /// <summary>
        /// HTTP クライアント実行オブジェクト
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// API トークンを指定して新しいインスタンスを生成します。
        /// </summary>
        /// <param name="apiToken">API トークン</param>
        public ApiClient(string apiToken)
        {
            _httpClient = new HttpClient(new AuthenticationHandler(apiToken));
        }

        /// <summary>
        /// 指定した HTTP リクエスト メッセージを送信します。
        /// </summary>
        /// <param name="requestMessage">HTTP リクエスト メッセージ</param>
        /// <returns>API のレスポンス ボディを文字列へ変換して返します。</returns>
        /// <exception cref="ApiRequestException">API の応答結果ステータスがエラーの場合にスローされます。</exception>
        private async Task<string> SendAsync(HttpRequestMessage requestMessage)
        {
            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode) throw new ApiRequestException(response.StatusCode, response.ReasonPhrase);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// GET メソッドを送信し、その応答を非同期で取得します。
        /// </summary>
        /// <param name="path">リクエスト パス</param>
        /// <param name="parameters">リクエスト パラメータ</param>
        /// <returns>API のレスポンス ボディを文字列へ変換して返します。</returns>
        /// <exception cref="ApiRequestException">API の応答結果ステータスがエラーの場合にスローされます。</exception>
        public async Task<string> GetAsync(string path, params (string key, object value)[] parameters)
        {
            var requestUri     = RequestUriGenerator.Generate(BaseUri, path, parameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            return await SendAsync(requestMessage);
        }

        /// <summary>
        /// POST メソッドを送信し、その応答を非同期で取得します。
        /// </summary>
        /// <param name="path">リクエスト パス</param>
        /// <param name="parameters">リクエスト パラメータ</param>
        /// <returns>API のレスポンス ボディを文字列へ変換して返します。</returns>
        /// <exception cref="ApiRequestException">API の応答結果ステータスがエラーの場合にスローされます。</exception>
        public async Task<string> PostAsync(string path, params (string key, object value)[] parameters)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseUri}{path}")
                                 {
                                     Content = new FormUrlEncodedContent(ConvertToParameters(parameters))
                                 };

            return await SendAsync(requestMessage);
        }

        /// <summary>
        /// PUT メソッドを送信し、その応答を非同期で取得します。
        /// </summary>
        /// <param name="path">リクエスト パス</param>
        /// <param name="parameters">リクエスト パラメータ</param>
        /// <returns>API のレスポンス ボディを文字列へ変換して返します。</returns>
        /// <exception cref="ApiRequestException">API の応答結果ステータスがエラーの場合にスローされます。</exception>
        public async Task<string> PutAsync(string path, params (string key, object value)[] parameters)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"{BaseUri}{path}")
                                 {
                                     Content = new FormUrlEncodedContent(ConvertToParameters(parameters))
                                 };

            return await SendAsync(requestMessage);
        }

        /// <summary>
        /// DELETE メソッドを送信し、その応答を非同期で取得します。
        /// </summary>
        /// <param name="path">リクエスト パス</param>
        /// <param name="parameters">リクエスト パラメータ</param>
        /// <returns>API のレスポンス ボディを文字列へ変換して返します。</returns>
        /// <exception cref="ApiRequestException">API の応答結果ステータスがエラーの場合にスローされます。</exception>
        public async Task<string> DeleteAsync(string path, params (string key, object value)[] parameters)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{BaseUri}{path}")
                                 {
                                     Content = new FormUrlEncodedContent(ConvertToParameters(parameters))
                                 };

            return await SendAsync(requestMessage);
        }

        /// <summary>
        /// <c>key</c> と <c>value</c> で定義された Tuple 配列のパラメータを <see cref="KeyValuePair{TKey,TValue}"/> のシーケンスへ変換します。
        /// </summary>
        /// <param name="parameters">変換元のパラメータ</param>
        /// <returns>変換した結果を返します。</returns>
        internal static IEnumerable<KeyValuePair<string, string>> ConvertToParameters(params (string key, object value)[] parameters)
            => parameters.Where(x => x.value != null)
                         .Select(x => new KeyValuePair<string, string>(x.key, ConvertToValue(x.value)));

        /// <summary>
        /// <see cref="object"/> 型の値を API 呼び出し用の適切な文字列へ変換します。
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <returns>変換結果文字列を返します。</returns>
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