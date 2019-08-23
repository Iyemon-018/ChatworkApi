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
        private async Task<TModel> GetAsync<TModel>(string path, params (string key, object value)[] parameters)
        {
            var content = await _apiClient.GetAsync(path, parameters);

            return JsonConvert.DeserializeObject<TModel>(content);
        }

        private async Task<TModel> PostAsync<TModel>(string path, params (string key, object value)[] parameters)
        {
            var content = await _apiClient.PostAsync(path, parameters);

            return JsonConvert.DeserializeObject<TModel>(content);
        }

        #region IMeApi

        /// <summary>
        /// 自分自身の情報を非同期で取得します。
        /// </summary>
        /// <returns>自分自身の情報を返します。</returns>
        public Task<Me> GetMeAsync() => GetAsync<Me>("/me");

        private static IDictionary<TaskStatus, string> TaskStatusToValueMap
            => new Dictionary<TaskStatus, string>
               {
                   {TaskStatus.Open, "open"}
                 , {TaskStatus.Done, "done"}
               };

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
        public Task<IEnumerable<MyTask>> GetMyTasksAsync(int? assignedByAccountId, TaskStatus status)
            => GetAsync<IEnumerable<MyTask>>("/my/status"
                                           , ("assigned_by_account_id", $"{assignedByAccountId}")
                                           , ("status", TaskStatusToValueMap[status]));

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

        private static readonly Dictionary<GroupChatIconType?, string> GroupChatIconTypeToValueMap
            = new Dictionary<GroupChatIconType?, string>
              {
                  {null, null}
                , {GroupChatIconType.Group, "group"}
                , {GroupChatIconType.Check, "check"}
                , {GroupChatIconType.Document, "document"}
                , {GroupChatIconType.Meeting, "meeting"}
                , {GroupChatIconType.Event, "event"}
                , {GroupChatIconType.Project, "project"}
                , {GroupChatIconType.Business, "business"}
                , {GroupChatIconType.Study, "study"}
                , {GroupChatIconType.Security, "security"}
                , {GroupChatIconType.Star, "star"}
                , {GroupChatIconType.Idea, "idea"}
                , {GroupChatIconType.Heart, "heart"}
                , {GroupChatIconType.Magcup, "magcup"}
                , {GroupChatIconType.Beer, "beer"}
                , {GroupChatIconType.Music, "music"}
                , {GroupChatIconType.Sports, "sports"}
                , {GroupChatIconType.Travel, "travel"}
              };

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
                                    , ("icon_preset", GroupChatIconTypeToValueMap[iconType])
                                    , ("link", link)
                                    , ("link_code", linkCode)
                                    , ("link_need_acceptance", needLinkAcceptance));

        #endregion
    }
}