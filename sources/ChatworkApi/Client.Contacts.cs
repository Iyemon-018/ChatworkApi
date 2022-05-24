namespace ChatworkApi;

using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api;
using Models;

public partial class Client : IContactsApi
{
    /// <summary>
    /// コンタクトの一覧を非同期で取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分自身の全てのコンタクトを返します。</returns>
    async Task<ChatworkResponse<IEnumerable<Contact>>> IContactsApi.GetContactsAsync(CancellationToken cancellationToken)
    {
        var request  = RestApiRequest.AsNonContent("/contacts");
        var response = await _apiClient.GetAsync(request, cancellationToken).ConfigureAwait(false);

        return await CreateResponseAsync<IEnumerable<Contact>>(response, HttpStatusCode.OK, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
