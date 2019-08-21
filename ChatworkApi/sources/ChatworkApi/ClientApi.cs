namespace ChatworkApi
{
    using System.Threading.Tasks;
    using Api;
    using Models;
    using Newtonsoft.Json;

    public sealed class ClientApi : IMeApi
    {
        private readonly ApiClient _apiClient;

        public IMeApi Me => this;

        public ClientApi(string apiToken)
        {
            _apiClient = new ApiClient(apiToken);
        }

        Task<MeModel> IMeApi.GetAsync() => GetAsync<MeModel>("/me");

        private async Task<TModel> GetAsync<TModel>(string path, params (string key, string value)[] parameters)
        {
            var content = await _apiClient.GetAsync(path, parameters);

            return JsonConvert.DeserializeObject<TModel>(content);
        }
    }
}