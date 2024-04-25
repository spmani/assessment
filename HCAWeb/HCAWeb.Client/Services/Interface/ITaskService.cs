using HCA.Model;
using HCA.Model.HCAUI;

namespace HCAWeb.Client.Services.Interface
{
    public interface ITaskService
    {
        UiResponse AddTask(TaskRequest taskRequest);

        IEnumerable<TaskDetailsResponse> GetTasks();
    }
}
