namespace ChatworkApi.Api
{
    using System.Threading.Tasks;
    using Models;

    public interface IMyApi
    {
        Task<MyStatusModel> GetStatusAsync();

        Task<MyTaskModel> GetTasksAsync(int? assignedByAccountId, ChatworkApi.TaskStatus status);
    }
}