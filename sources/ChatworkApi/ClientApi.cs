namespace ChatworkApi
{
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
    public sealed class ClientApi : IMeApi, IMyApi, IContactsApi
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

        /// <summary>
        /// コンタクトの一覧を非同期で取得します。
        /// </summary>
        /// <returns>自分自身の全てのコンタクトを返します。</returns>
        public Task<IEnumerable<Contact>> GetContactsAsync() => GetAsync<IEnumerable<Contact>>("/contacts");
    }
}