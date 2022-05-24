namespace ChatworkApi;

using System.Net.Http;
using System.Threading;
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
    /// HTTP クライアント実行オブジェクト
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// API トークンを指定して新しいインスタンスを生成します。
    /// </summary>
    /// <param name="httpClient">API トークン</param>
    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<RestApiResponse> GetAsync(RestApiRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(request.Uri(), cancellationToken).ConfigureAwait(false);
        return new RestApiResponse(response);
    }
    
    public async Task<RestApiResponse> PostAsync(RestApiRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync(request.Uri(), request.Content, cancellationToken).ConfigureAwait(false);
        return new RestApiResponse(response);
    }

    public async Task<RestApiResponse> PutAsync(RestApiRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsync(request.Uri(), request.Content, cancellationToken).ConfigureAwait(false);
        return new RestApiResponse(response);
    }

    public async Task<RestApiResponse> DeleteAsync(RestApiRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync(request.Uri(), cancellationToken).ConfigureAwait(false);
        return new RestApiResponse(response);
    }
    
    public void ApiToken(string apiToken)
        => _httpClient.DefaultRequestHeaders.Add("X-ChatWorkToken", apiToken);
}