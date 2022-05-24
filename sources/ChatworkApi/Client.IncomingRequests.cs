namespace ChatworkApi;

using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api;
using Models;

public partial class Client : IIncomingRequestsApi
{
    /// <summary>
    /// 自分に対するコンタクト承認依頼を取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分に対するコンタクト承認依頼を返します。</returns>
    async Task<ChatworkResponse<IEnumerable<ContactApprovalRequest>>> IIncomingRequestsApi.GetContactApprovalRequestsAsync(CancellationToken cancellationToken)
    {
        var request  = RestApiRequest.AsNonContent("/incoming_requests");
        var response = await _apiClient.GetAsync(request, cancellationToken).ConfigureAwait(false);

        return await CreateResponseAsync<IEnumerable<ContactApprovalRequest>>(response, HttpStatusCode.OK, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 自分に対するコンタクト承認依頼を承認します。
    /// </summary>
    /// <param name="requestId">承認依頼ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>承認したコンタクト承認依頼を返します。</returns>
    async Task<ChatworkResponse<ConfirmedContactApprovalResponse>> IIncomingRequestsApi.ConfirmContactApprovalRequestAsync(int requestId, CancellationToken cancellationToken)
    {
        var request  = RestApiRequest.AsNonContent($"/incoming_requests/{requestId}");
        var response = await _apiClient.PutAsync(request, cancellationToken).ConfigureAwait(false);

        return await CreateResponseAsync<ConfirmedContactApprovalResponse>(response, HttpStatusCode.OK, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 自分に対するコンタクト承認依頼をキャンセルします。
    /// </summary>
    /// <param name="requestId">キャンセルするコンタクト承認依頼ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task<ChatworkResponse<EmptyResponse>> IIncomingRequestsApi.CancelContactApprovalRequestAsync(int requestId, CancellationToken cancellationToken)
    {
        var request  = RestApiRequest.AsNonContent($"/incoming_requests/{requestId}");
        var response = await _apiClient.DeleteAsync(request, cancellationToken).ConfigureAwait(false);

        return await CreateResponseAsync<EmptyResponse>(response, HttpStatusCode.OK, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
