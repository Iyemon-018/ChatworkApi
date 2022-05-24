namespace ChatworkApi.Api;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models;

/// <summary>
/// /contacts API を呼び出すための機能を提供するインターフェースです。
/// </summary>
public interface IContactsApi
{
    /// <summary>
    /// コンタクトの一覧を非同期で取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分自身の全てのコンタクトを返します。</returns>
    Task<ChatworkResponse<IEnumerable<Contact>>> GetContactsAsync(CancellationToken cancellationToken = default);
}