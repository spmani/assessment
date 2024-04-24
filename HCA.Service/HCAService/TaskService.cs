using HCA.Model;
using HCA.Repository.IHCARepo;
using HCA.Service.IHCAService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Service.HCAService
{
    internal class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private readonly ITaskRepo _taskRepo;
        public TaskService(ILogger<TaskService> logger,ITaskRepo taskRepo)
        {
            _logger = logger;
            _taskRepo = taskRepo;
        }
        public List<TaskDetailsResponse> GetAllTask()
        {
            List<TaskDetailsResponse> resultData = null;
            try
            {
                resultData = _taskRepo.GetAllTask();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Task Service GetAllTask error:{ex.Message}");
            }
            return resultData.ToList();
        }


        public TaskResponse InsertUpdateTask(TaskRequest objTaskRequest)
        {
            TaskResponse resultData = null;
            try
            {
                resultData = _taskRepo.InsertUpdateTask(objTaskRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Task Service InsertUpdateTask error:{ex.Message}");
            }
            return resultData;
        }

        public List<TaskDetailsResponse> GetTask(int TaskId)
        {
            List<TaskDetailsResponse> resultData = null;
            try
            {
                resultData = _taskRepo.GetTask(TaskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Task Service GetTask error:{ex.Message}");
            }
            return resultData.ToList();
        }

        public long DeleteTask(int TaskId)
        {
            long result = -1;
            try
            {
                result = _taskRepo.DeleteTask(TaskId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Task Service DeleteTask error:{ex.Message}");
            }
            if (result == null)

                _logger.LogInformation($"Returning product(s): {result}");
            return result;
        }
    }
}
