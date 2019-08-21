namespace ChatworkApi
{
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;

    internal interface IApiService
    {
        
    }

    public sealed class ChatworkClient : IApiService
    {
        private readonly ApiClient _apiClient;

        public ChatworkClient(string apiToken)
        {
            _apiClient = new ApiClient(apiToken);
        }

        public async Task<IMeModel> GetMeAsync()
        {
            var content = await _apiClient.GetAsync("/me").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<MeModel>(content);
        }
    }
}