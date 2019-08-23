namespace ChatworkApi.Api
{
    using System;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// /room API 機能を提供するためのインターフェースです。
    /// </summary>
    /// <remarks>
    /// http://developer.chatwork.com/ja/endpoint_rooms.html#GET-rooms
    /// </remarks>
    public interface IRoomsApi
    {
        /// <summary>
        /// 自分のチャット一覧を非同期で取得します。
        /// </summary>
        /// <returns>自分のチャット情報を全て返します。</returns>
        Task<MyRoom> GetMyRoomsAsync();

        /// <summary>
        /// グループチャットを新規作成します。
        /// </summary>
        /// <param name="name">作成したいグループチャットの名称</param>
        /// <param name="adminMembersIds">管理者権限を持つユーザーのIDリスト</param>
        /// <param name="memberMembersIds">メンバー権限を持つユーザーのIDリスト</param>
        /// <param name="readonlyMembersIds">閲覧権限のみ持つユーザーのIDリスト</param>
        /// <param name="description">グループチャットに表示する概要説明</param>
        /// <param name="iconType">グループチャット アイコンの種類</param>
        /// <param name="link">招待リンクを作成するかどうか（<c>true</c> の場合、作成します。<c>false</c> もしくは <c>null</c> の場合、作成しません。）</param>
        /// <param name="linkCode">リンクのパス部分の文字列（省略した場合はランダムな文字列となります。）</param>
        /// <param name="needLinkAcceptance">作成したグループチャットに参加する場合は管理者の承認を必要とするかどうか（<c>true</c> の場合、承認が必要になります。<c>false</c> もしくは、<c>null</c> の場合、承認は不要となります。）</param>
        /// <returns>作成したグループチャットの ID を返します。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> が <c>null</c> もしくは <c>string.Empty</c> の場合にスローされます。</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="adminMembersIds"/> の要素数が <c>1</c> 未満の場合にスローされます。</exception>
        Task<CreatedRoom> CreateNewRoomAsync(string             name
                                           , int[]              adminMembersIds
                                           , int[]              memberMembersIds
                                           , int[]              readonlyMembersIds
                                           , string             description
                                           , GroupChatIconType? iconType
                                           , bool?              link
                                           , string             linkCode
                                           , bool?              needLinkAcceptance);
    }
}