namespace ChatworkApi.Api
{
    using System.Collections.Generic;
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
        /// <returns>自分自身の全てのコンタクトを返します。</returns>
        Task<IEnumerable<Contact>> GetContactsAsync();
    }
}