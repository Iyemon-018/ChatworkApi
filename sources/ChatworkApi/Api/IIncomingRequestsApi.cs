namespace ChatworkApi.Api;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models;

/// <summary>
/// /incoming_requests API 機能を提供するためのインターフェースです。
/// </summary>
/// <remarks>
/// http://developer.chatwork.com/ja/endpoint_incoming_requests.html
/// </remarks>
public interface IIncomingRequestsApi
{
    /// <summary>
    /// 自分に対するコンタクト承認依頼を取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分に対するコンタクト承認依頼を返します。</returns>
    Task<ChatworkResponse<IEnumerable<ContactApprovalRequest>>> GetContactApprovalRequestsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 自分に対するコンタクト承認依頼を承認します。
    /// </summary>
    /// <param name="requestId">承認依頼ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>承認したコンタクト承認依頼を返します。</returns>
    Task<ChatworkResponse<ConfirmedContactApprovalResponse>> ConfirmContactApprovalRequestAsync(int requestId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 自分に対するコンタクト承認依頼をキャンセルします。
    /// </summary>
    /// <param name="requestId">キャンセルするコンタクト承認依頼ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ChatworkResponse<EmptyResponse>> CancelContactApprovalRequestAsync(int requestId, CancellationToken cancellationToken = default);
}