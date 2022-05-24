namespace ChatworkApi;

using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api;
using Models;

public partial class Client : IMeApi
{
    /// <summary>
    /// 自分自身の情報を非同期で取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分自身の情報を返します。</returns>
    async Task<ChatworkResponse<Me>> IMeApi.GetMeAsync(CancellationToken cancellationToken)
    {
        var request  = RestApiRequest.AsNonContent("/me");
        var response = await _apiClient.GetAsync(request, cancellationToken).ConfigureAwait(false);

        return await CreateResponseAsync<Me>(response, HttpStatusCode.OK, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}