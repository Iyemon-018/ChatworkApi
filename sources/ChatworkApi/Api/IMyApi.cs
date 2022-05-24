namespace ChatworkApi.Api;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models;
using TaskStatus = ChatworkApi.TaskStatus;

/// <summary>
/// /my API を実行するための機能を提供するインターフェースです。
/// </summary>
public interface IMyApi
{
    /// <summary>
    /// 自分自身のステータスを非同期で取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分自身のステータスを返します。</returns>
    Task<ChatworkResponse<MyStatus>> GetMyStatusAsync(CancellationToken cancellationToken = default);

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
    Task<ChatworkResponse<IEnumerable<MyTask>>> GetMyTasksAsync(int? assignedByAccountId, TaskStatus status, CancellationToken cancellationToken = default);
}