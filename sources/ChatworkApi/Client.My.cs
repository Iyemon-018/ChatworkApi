namespace ChatworkApi;

using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api;
using Models;

public partial class Client : IMyApi
{
    /// <summary>
    /// 自分自身のステータスを非同期で取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分自身のステータスを返します。</returns>
    async Task<ChatworkResponse<MyStatus>> IMyApi.GetMyStatusAsync(CancellationToken cancellationToken)
    {
        var request  = RestApiRequest.AsNonContent("/my/status");
        var response = await _apiClient.GetAsync(request, cancellationToken).ConfigureAwait(false);

        return await CreateResponseAsync<MyStatus>(response, HttpStatusCode.OK, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 自分自身に割り当てられたタスクを非同期で取得します。
    /// </summary>
    /// <param name="assignedByAccountId">
    ///     タスクをアサインしたアカウントの ID です。
    ///     <c>null</c> を指定した場合、全アカウントからのタスクを取得します。
    /// </param>
    /// <param name="status">タスクの状態</param>
    /// <param name="cancellationToken"></param>
    /// <returns>自分自身に割り当てられたステータスを返します。</returns>
    async Task<ChatworkResponse<IEnumerable<MyTask>>> IMyApi.GetMyTasksAsync(int?              assignedByAccountId
                                                                           , TaskStatus        status
                                                                           , CancellationToken cancellationToken)
    {
        var request = RestApiRequest.AsQuery("/my/tasks"
              , new Dictionary<string, object>
                {
                    ["assigned_by_account_id"] = assignedByAccountId
                  , ["status"]                 = status.ToAlias()
                });
        var response = await _apiClient.GetAsync(request, cancellationToken).ConfigureAwait(false);

        return await CreateResponseAsync<IEnumerable<MyTask>>(response, HttpStatusCode.OK, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}