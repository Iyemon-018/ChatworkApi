namespace ChatworkApi.Api
{
    using System.Threading.Tasks;
    using Models;

    public interface IMeApi
    {
        Task<MeModel> GetAsync();
    }
}