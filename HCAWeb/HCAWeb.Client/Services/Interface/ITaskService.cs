using HCA.Model;

namespace HCAWeb.Client.Services.Interface
{
    public interface ITaskService
    {
        bool AddTask(TaskRequest taskRequest);

        IEnumerable<TaskDetailsResponse> GetTasks();
    }
}
