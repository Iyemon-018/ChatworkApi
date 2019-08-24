namespace ChatworkApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Chatwork API を呼び出すためのクライアント クラスです。
    /// Chatwork へのアクセスにはこのクラスを使用してください。
    /// </summary>
    /// <seealso cref="IMeApi"/>
    /// <seealso cref="IMyApi"/>
    /// <seealso cref="IContactsApi"/>
    public sealed class ClientApi : IMeApi, IMyApi, IContactsApi, IRoomsApi
    {
        /// <summary>
        /// Web API を使用するためのクライアント機能です。
        /// </summary>
        private readonly ApiClient _apiClient;

        /// <summary>
        /// API トークンを指定してクライアントのインスタンスをを生成します。
        /// </summary>
        /// <param name="apiToken">API トークン</param>
        public ClientApi(string apiToken)
        {
            _apiClient = new ApiClient(apiToken);
        }

        /// <summary>
        /// API を実行して、応答のあったデータを変換します。
        /// </summary>
        /// <typeparam name="TModel">変換するデータの型</typeparam>
        /// <param name="path">実行する API のパス</param>
        /// <param name="parameters">
        /// API パラメータを指定します。
        /// <c>key</c> はパラメータのキーを指定します。<c>value</c> は <c>key</c> の値を指定します。
        /// </param>
        private async Task<TModel> GetAsync<TModel>(string                              path
                                                  , params (string key, object value)[] parameters)
        {
            var content = await _apiClient.GetAsync(path, parameters);

            return JsonConvert.DeserializeObject<TModel>(content);
        }

        private async Task<TModel> PostAsync<TModel>(string                              path
                                                   , params (string key, object value)[] parameters)
        {
            var content = await _apiClient.PostAsync(path, parameters);

            return JsonConvert.DeserializeObject<TModel>(content);
        }

        private async Task<TModel> PutAsync<TModel>(string                              path
                                                  , params (string key, object value)[] parameters)
        {
            var content = await _apiClient.PutAsync(path, parameters);

            return JsonConvert.DeserializeObject<TModel>(content);
        }

        private async Task DeleteAsync(string                              path
                                     , params (string key, object value)[] parameters)
        {
            await _apiClient.DeleteAsync(path, parameters);
        }

        #region IMeApi

        /// <summary>
        /// 自分自身の情報を非同期で取得します。
        /// </summary>
        /// <returns>自分自身の情報を返します。</returns>
        public Task<Me> GetMeAsync() => GetAsync<Me>("/me");

        #endregion

        #region IMyApi

        /// <summary>
        /// 自分自身のステータスを非同期で取得します。
        /// </summary>
        /// <returns>自分自身のステータスを返します。</returns>
        public Task<MyStatus> GetMyStatusAsync() => GetAsync<MyStatus>("/my/status");

        /// <summary>
        /// 自分自身に割り当てられたタスクを非同期で取得します。
        /// </summary>
        /// <param name="assignedByAccountId">
        /// タスクをアサインしたアカウントの ID です。
        /// <c>null</c> を指定した場合、全アカウントからのタスクを取得します。
        /// </param>
        /// <param name="status">タスクの状態</param>
        /// <returns>自分自身に割り当てられたステータスを返します。</returns>
        public Task<IEnumerable<MyTask>> GetMyTasksAsync(int?       assignedByAccountId
                                                       , TaskStatus status)
            => GetAsync<IEnumerable<MyTask>>("/my/status"
                                           , ("assigned_by_account_id", $"{assignedByAccountId}")
                                           , ("status", status.ToParameterValue()));

        #endregion

        #region IContactsApi

        /// <summary>
        /// コンタクトの一覧を非同期で取得します。
        /// </summary>
        /// <returns>自分自身の全てのコンタクトを返します。</returns>
        public Task<IEnumerable<Contact>> GetContactsAsync() => GetAsync<IEnumerable<Contact>>("/contacts");

        #endregion

        #region IRoomsApi

        /// <summary>
        /// 自分のチャット一覧を非同期で取得します。
        /// </summary>
        /// <returns>自分のチャット情報を全て返します。</returns>
        public Task<MyRoom> GetMyRoomsAsync() => GetAsync<MyRoom>("/rooms");

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
        public Task<CreatedRoom> CreateNewRoomAsync(string             name
                                                  , int[]              adminMembersIds
                                                  , int[]              memberMembersIds
                                                  , int[]              readonlyMembersIds
                                                  , string             description
                                                  , GroupChatIconType? iconType
                                                  , bool?              link
                                                  , string             linkCode
                                                  , bool?              needLinkAcceptance)
            => PostAsync<CreatedRoom>("/rooms"
                                    , ("name", name)
                                    , ("members_admin_ids", adminMembersIds)
                                    , ("members_member_ids", memberMembersIds)
                                    , ("members_readonly_ids", readonlyMembersIds)
                                    , ("description", description)
                                    , ("icon_preset", iconType?.ToParameterValue())
                                    , ("link", link)
                                    , ("link_code", linkCode)
                                    , ("link_need_acceptance", needLinkAcceptance));

        /// <summary>
        /// 指定したグループチャットの構成を取得します。
        /// </summary>
        /// <param name="roomId">取得するグループチャットのID</param>
        /// <returns>グループチャットの構成情報を返します。</returns>
        public Task<RoomConfiguration> GetRoomConfigurationAsync(int roomId)
            => GetAsync<RoomConfiguration>($"/rooms/{roomId}");

        /// <summary>
        /// 指定したグループチャットの構成を更新します。
        /// </summary>
        /// <param name="roomId">更新したいグループチャットのID</param>
        /// <param name="name">グループチャットの名称</param>
        /// <param name="description">グループチャットの概要説明</param>
        /// <param name="iconType">グループチャットに表示するアイコンの種類</param>
        /// <returns>更新したグループチャットの構成を取得します。</returns>
        public Task<UpdatedRoomConfiguration> UpdateRoomConfigurationAsync(int                roomId
                                                                         , string             name
                                                                         , string             description
                                                                         , GroupChatIconType? iconType)
            => PutAsync<UpdatedRoomConfiguration>($"/rooms/{roomId}"
                                                , ("name", name)
                                                , ("description", description)
                                                , ("icon_preset", iconType?.ToParameterValue()));

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
        public Task LeavingRoomAsync(int               roomId
                                   , LeavingRoomAction action)
            => DeleteAsync($"/rooms/{roomId}"
                         , ("action_type", action.ToParameterValue()));

        /// <summary>
        /// 指定したグループチャットに参加しているメンバーの情報を取得します。
        /// </summary>
        /// <param name="roomId">取得対象のグループチャットのID</param>
        /// <returns>グループチャットに参加しているメンバーの情報シーケンスを返します。</returns>
        public Task<IEnumerable<RoomMember>> GetRoomMembersAsync(int roomId)
            => GetAsync<IEnumerable<RoomMember>>($"/rooms/{roomId}");

        /// <summary>
        /// 指定したグループチャットのメンバーを更新します。
        /// </summary>
        /// <param name="roomId">変更対象のグループチャットのID</param>
        /// <param name="adminMembersIds">管理者権限に設定するユーザーのIDリスト</param>
        /// <param name="memberMembersIds">メンバー権限を持つユーザーのIDリスト</param>
        /// <param name="readonlyMembersIds">読み取り専用権限のみ持つユーザーのIDリスト</param>
        /// <returns>メンバーの更新結果を返します。</returns>
        public Task<UpdatedRoomMember> UpdateRoomMembersAsync(int   roomId
                                                            , int[] adminMembersIds
                                                            , int[] memberMembersIds
                                                            , int[] readonlyMembersIds)
            => PutAsync<UpdatedRoomMember>($"/rooms/{roomId}"
                                         , ("members_admin_ids", adminMembersIds)
                                         , ("members_member_ids", memberMembersIds)
                                         , ("members_readonly_ids", readonlyMembersIds));

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
        public Task<IEnumerable<Message>> GetMessagesAsync(int  roomId
                                                         , int? force)
            => GetAsync<IEnumerable<Message>>($"/rooms/{roomId}/messages"
                                            , ("force", force));

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
        public Task<AddMessage> AddMessageAsync(int    roomId
                                              , string body
                                              , bool?  unread)
            => PostAsync<AddMessage>($"/rooms/{roomId}/messages"
                                   , ("body", body)
                                   , ("self_unread", unread));

        /// <summary>
        /// 指定したグループチャットのタスク一覧を取得します。
        /// </summary>
        /// <param name="roomId">取得対象のグループチャットID</param>
        /// <param name="accountId">タスクの担当者に設定されているアカウントID</param>
        /// <param name="assignedByAccountId">タスクの依頼者のアカウントID</param>
        /// <param name="status">取得するタスクのステータス</param>
        /// <returns>タスクの一覧を返します。</returns>
        public Task<IEnumerable<RoomTask>> GetRoomTasksAsync(int         roomId
                                                           , int?        accountId
                                                           , int?        assignedByAccountId
                                                           , TaskStatus? status)
            => GetAsync<IEnumerable<RoomTask>>($"/rooms/{roomId}/tasks"
                                             , ("account_id", accountId)
                                             , ("assigned_by_account_id", assignedByAccountId)
                                             , ("status", status?.ToParameterValue()));

        /// <summary>
        /// 指定したグループチャットにタスクを追加します。
        /// </summary>
        /// <param name="roomId">追加対象のグループチャットID</param>
        /// <param name="body">タスクの内容</param>
        /// <param name="assignToIds">タスクの担当者のアカウントIDリスト</param>
        /// <param name="limitType">タスクの期限種別</param>
        /// <param name="limit">タスクの期限</param>
        /// <returns>追加したタスクの情報を返します。</returns>
        public Task<AddTask> AddTaskAsync(int            roomId
                                        , string         body
                                        , int[]          assignToIds
                                        , TaskLimitType? limitType
                                        , DateTime?      limit)
            => PostAsync<AddTask>($"/rooms/{roomId}/tasks"
                                , ("body", body)
                                , ("to_ids", assignToIds)
                                , ("limit_type", limitType?.ToParameterValue())
                                , ("limit", limit));

        /// <summary>
        /// 指定したグループチャットのタスク情報を取得します。
        /// </summary>
        /// <param name="roomId">取得対象のグループチャットのID</param>
        /// <param name="taskId">取得したいタスクのID</param>
        /// <returns>タスク情報を返します。</returns>
        public Task<RoomTask> GetRoomTaskAsync(int roomId
                                             , int taskId)
            => GetAsync<RoomTask>($"/rooms/{roomId}/tasks/{taskId}");

        /// <summary>
        /// 指定したグループチャットのタスクの状態を更新します。
        /// </summary>
        /// <param name="roomId">更新対象のタスクのあるグループチャットのID</param>
        /// <param name="taskId">更新対象のタスクID</param>
        /// <param name="status">更新後のタスク状態</param>
        /// <returns>更新したタスク情報を返します。</returns>
        public Task<UpdatedTaskStatus> UpdateTaskStatusAsync(int        roomId
                                                           , int        taskId
                                                           , TaskStatus status)
            => PutAsync<UpdatedTaskStatus>($"/rooms/{roomId}/tasks/{taskId}"
                                         , ("body", status.ToParameterValue()));

        #endregion
    }
}