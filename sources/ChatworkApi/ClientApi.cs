namespace ChatworkApi
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api;
    using Models;
    using Newtonsoft.Json;

    public sealed class ClientApi : IMeApi, IMyApi, IContactsApi
    {
        private readonly ApiClient _apiClient;

        public IMeApi Me => this;

        public IMyApi My => this;

        public IContactsApi Contacts => this;

        private static IDictionary<TaskStatus, string> TaskStatusToValueMap
            => new Dictionary<TaskStatus, string>
               {
                   {TaskStatus.Open, "open"}
                 , {TaskStatus.Done, "done"}
               };

        public ClientApi(string apiToken)
        {
            _apiClient = new ApiClient(apiToken);
        }

        Task<MeModel> IMeApi.GetAsync() => GetAsync<MeModel>("/me");

        private async Task<TModel> GetAsync<TModel>(string path, params (string key, object value)[] parameters)
        {
            var content = await _apiClient.GetAsync(path, parameters);

            return JsonConvert.DeserializeObject<TModel>(content);
        }

        Task<MyStatusModel> IMyApi.GetStatusAsync() => GetAsync<MyStatusModel>("/my/status");

        Task<MyTaskModel> IMyApi.GetTasksAsync(int? assignedByAccountId, TaskStatus status)
        => GetAsync<MyTaskModel>("/my/status"
                                   ,  ("assigned_by_account_id", $"{assignedByAccountId}")
                                   , ("status", TaskStatusToValueMap[status]));

        Task<IEnumerable<ContactModel>> IContactsApi.GetAsync() => GetAsync<IEnumerable<ContactModel>>("/contacts");
    }
}