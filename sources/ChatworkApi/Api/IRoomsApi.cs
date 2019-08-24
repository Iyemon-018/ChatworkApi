namespace ChatworkApi.Api
{
    using System;
    using System.Collections.Generic;
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
        Task<IEnumerable<MyRoom>> GetMyRoomsAsync();

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
        Task<CreatedRoom> CreateNewRoomAsync(string name
                                           , int[] adminMembersIds
                                           , int[] memberMembersIds
                                           , int[] readonlyMembersIds
                                           , string description
                                           , GroupChatIconType? iconType
                                           , bool? link
                                           , string linkCode
                                           , bool? needLinkAcceptance);

        /// <summary>
        /// 指定したグループチャットの構成を取得します。
        /// </summary>
        /// <param name="roomId">取得するグループチャットのID</param>
        /// <returns>グループチャットの構成情報を返します。</returns>
        Task<RoomConfiguration> GetRoomConfigurationAsync(int roomId);

        /// <summary>
        /// 指定したグループチャットの構成を更新します。
        /// </summary>
        /// <param name="roomId">更新したいグループチャットのID</param>
        /// <param name="name">グループチャットの名称</param>
        /// <param name="description">グループチャットの概要説明</param>
        /// <param name="iconType">グループチャットに表示するアイコンの種類</param>
        /// <returns>更新したグループチャットの構成を取得します。</returns>
        Task<UpdatedRoomConfiguration> UpdateRoomConfigurationAsync(int roomId
                                                                  , string name
                                                                  , string description
                                                                  , GroupChatIconType? iconType);

        /// <summary>
        /// 指定したグループチャットから退室します。
        /// </summary>
        /// <param name="roomId">退室するグループチャットのID</param>
        /// <param name="action">
        /// 退室する際のアクションを指定します。<br/>
        /// <see cref="LeavingRoomAction.Leave"/> を選択すると、グループチャット内にある自分が担当のタスク、及び自分が送信したファイルは削除されます。
        /// <see cref="LeavingRoomAction.Delete"/> を選択すると、グループチャットに参加しているメンバー全員のメッセージ、タスク、ファイルがすべて削除されます。
        /// </param>
        /// <returns>非同期タスクを返します。</returns>
        Task LeavingRoomAsync(int roomId
                            , LeavingRoomAction action);

        /// <summary>
        /// 指定したグループチャットに参加しているメンバーの情報を取得します。
        /// </summary>
        /// <param name="roomId">取得対象のグループチャットのID</param>
        /// <returns>グループチャットに参加しているメンバーの情報シーケンスを返します。</returns>
        Task<IEnumerable<RoomMember>> GetRoomMembersAsync(int roomId);

        /// <summary>
        /// 指定したグループチャットのメンバーを更新します。
        /// </summary>
        /// <param name="roomId">変更対象のグループチャットのID</param>
        /// <param name="adminMembersIds">管理者権限に設定するユーザーのIDリスト</param>
        /// <param name="memberMembersIds">メンバー権限を持つユーザーのIDリスト</param>
        /// <param name="readonlyMembersIds">読み取り専用権限のみ持つユーザーのIDリスト</param>
        /// <returns>メンバーの更新結果を返します。</returns>
        Task<UpdatedRoomMember> UpdateRoomMembersAsync(int roomId
                                                     , int[] adminMembersIds
                                                     , int[] memberMembersIds
                                                     , int[] readonlyMembersIds);

        /// <summary>
        /// 指定したグループチャットのメッセージを取得します。
        /// </summary>
        /// <param name="roomId">取得対象のグループチャットID</param>
        /// <param name="force">
        /// 最新の 100 件を取得するかどうか。
        /// <c>true</c> の場合、最新の 100 件を取得します。
        /// <c>false</c> もしくは <c>null</c> の場合、前回取得分からの差分を取得します。
        /// </param>
        /// <returns>最大 100 件までのメッセージを取得します。</returns>
        Task<IEnumerable<Message>> GetMessagesAsync(int roomId
                                                  , bool? force);

        /// <summary>
        /// 指定したグループチャットにメッセージを追加します。
        /// </summary>
        /// <param name="roomId">追加するグループチャットのID</param>
        /// <param name="body">
        /// メッセージの本文<br/>
        /// http://developer.chatwork.com/ja/messagenotation.html
        /// </param>
        /// <param name="unread">
        /// メッセージを未読とするかどうか。<c>true</c> の場合、未読にします。<c>false</c> もしくは、<c>null</c> の場合、既読にします。
        /// </param>
        /// <returns>メッセージを追加したグループチャットの構成情報を返します。</returns>
        Task<AddMessage> AddMessageAsync(int roomId
                                       , string body
                                       , bool? unread);

        // TODO /rooms/{room_id}/messages/read

        // TODO /rooms/{room_id}/messages/unread

        // TODO /rooms/{room_id}/messages/{message_id}

        // TODO /rooms/{room_id}/messages/{message_id}

        // TODO /rooms/{room_id}/messages/{message_id}

        /// <summary>
        /// 指定したグループチャットのタスク一覧を取得します。
        /// </summary>
        /// <param name="roomId">取得対象のグループチャットID</param>
        /// <param name="accountId">タスクの担当者に設定されているアカウントID</param>
        /// <param name="assignedByAccountId">タスクの依頼者のアカウントID</param>
        /// <param name="status">取得するタスクのステータス</param>
        /// <returns>タスクの一覧を返します。</returns>
        Task<IEnumerable<RoomTask>> GetRoomTasksAsync(int roomId
                                                    , int? accountId
                                                    , int? assignedByAccountId
                                                    , ChatworkApi.TaskStatus? status);

        /// <summary>
        /// 指定したグループチャットにタスクを追加します。
        /// </summary>
        /// <param name="roomId">追加対象のグループチャットID</param>
        /// <param name="body">タスクの内容</param>
        /// <param name="assignToIds">タスクの担当者のアカウントIDリスト</param>
        /// <param name="limitType">タスクの期限種別</param>
        /// <param name="limit">タスクの期限</param>
        /// <returns>追加したタスクの情報を返します。</returns>
        Task<AddTask> AddTaskAsync(int roomId
                                 , string body
                                 , int[] assignToIds
                                 , TaskLimitType? limitType
                                 , DateTime? limit);

        /// <summary>
        /// 指定したグループチャットのタスク情報を取得します。
        /// </summary>
        /// <param name="roomId">取得対象のグループチャットのID</param>
        /// <param name="taskId">取得したいタスクのID</param>
        /// <returns>タスク情報を返します。</returns>
        Task<RoomTask> GetRoomTaskAsync(int roomId
                                      , int taskId);

        /// <summary>
        /// 指定したグループチャットのタスクの状態を更新します。
        /// </summary>
        /// <param name="roomId">更新対象のタスクのあるグループチャットのID</param>
        /// <param name="taskId">更新対象のタスクID</param>
        /// <param name="status">更新後のタスク状態</param>
        /// <returns>更新したタスク情報を返します。</returns>
        Task<UpdatedTaskStatus> UpdateTaskStatusAsync(int roomId
                                                    , int taskId
                                                    , ChatworkApi.TaskStatus status);

        // TODO /rooms/{room_id}/files

        // TODO /rooms/{room_id}/files

        // TODO /rooms/{room_id}/files/{file_id}

        // TODO /rooms/{room_id}/link

        // TODO /rooms/{room_id}/link

        // TODO /rooms/{room_id}/link

        // TODO /rooms/{room_id}/link
    }
}