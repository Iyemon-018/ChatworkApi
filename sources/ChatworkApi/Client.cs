namespace ChatworkApi;

using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Api;
using Models;

/// <summary>
/// Chatwork API を呼び出すためのクライアント クラスです。
/// Chatwork へのアクセスにはこのクラスを使用してください。
/// </summary>
public sealed partial class Client : IClient
{
    /// <summary>
    /// Web API を使用するためのクライアント機能です。
    /// </summary>
    private readonly ApiClient _apiClient;

    /// <summary>
    /// API トークンを指定してクライアントのインスタンスをを生成します。
    /// </summary>
    /// <param name="httpClient">HTTP クライアント</param>
    public Client(HttpClient httpClient)
    {
        _apiClient = new ApiClient(httpClient);
    }

    IMeApi IClient.Me => this;

    IMyApi IClient.My => this;

    IContactsApi IClient.Contacts => this;

    IRoomsApi IClient.Rooms => this;

    IIncomingRequestsApi IClient.IncomingRequests => this;

    public void ApiToken(string apiToken) => _apiClient.ApiToken(apiToken);

    private async Task<ChatworkResponse<T>> CreateResponseAsync<T>(RestApiResponse       response
                                                                 , HttpStatusCode        successStatusCode
                                                                 , JsonSerializerOptions options           = default
                                                                 , CancellationToken     cancellationToken = default)
    {
        if (response.StatusCode == successStatusCode)
        {
            var content = await response.DeserializeAsync<T>(options: options ?? Constants.JsonSerializerOption, cancellationToken: cancellationToken).ConfigureAwait(false);
            return new ChatworkResponse<T>(content, response.RateLimit(), response.StatusCode, response.RequestMessage);
        }
        else
        {
            var error = await response.DeserializeAsync<Error>(options: options ?? Constants.JsonSerializerOption, cancellationToken: cancellationToken).ConfigureAwait(false);
            return new ChatworkResponse<T>(response.RateLimit(), response.StatusCode, response.RequestMessage, error);
        }
    }
}