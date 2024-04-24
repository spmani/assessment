using HCA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Service.IHCAService
{
    public interface ITaskService
    {
        public List<TaskDetailsResponse> GetAllTask();
        public TaskResponse InsertUpdateTask(TaskRequest objTask);
        public List<TaskDetailsResponse> GetTask(int TaskId);

        public long DeleteTask(int TaskId);
    }
}
