namespace ChatworkApi.Api;

using System.Threading;
using System.Threading.Tasks;
using Models;

/// <summary>
/// /me API を実行するための API を提供するためのインターフェースです。
/// </summary>
public interface IMeApi
{
    /// <summary>
    /// 自分自身の情報を非同期で取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分自身の情報を返します。</returns>
    Task<ChatworkResponse<Me>> GetMeAsync(CancellationToken cancellationToken = default);
}