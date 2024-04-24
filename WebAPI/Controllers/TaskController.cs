using HCA.Model;
using HCA.Service.IHCAService;
using HCA.Utitlities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;
        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;

        }

        [HttpGet]

        public Task<IActionResult> GetAllTask()
        {
            try
            {
                var returnedData = _taskService.GetAllTask();

                if (returnedData.Count > 0)
                {
                    return Task.FromResult<IActionResult>(Ok(new { Status = 0, Message = "GetAllTask returned successfully!", data = returnedData }));

                }
                else if (returnedData.Count == 0)
                {
                    _logger.LogInformation($"Get All Task Returned data was  null.");
                    return Task.FromResult<IActionResult>(Ok(new { Status = 1, Message = "Get Task Data was null!" }));

                }

                return Task.FromResult<IActionResult>(Ok(new { Status = -1, Message = " Something went Wrong!" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(Ok(new { Status = -1, Message = ex }));
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertUpdateTask([FromBody] TaskRequest objTask)
        {
            try
            {
                var data = _taskService.InsertUpdateTask(objTask);
                if (data != null)
                {
                    if (data.Result == (int)CRUDResponse.InsertSuccess)
                    {
                        return Ok(new { Status = ResponseStatus.Success, Message = "Task Details insert successfully!", Data = data });
                    }
                    else if (data.Result == (int)CRUDResponse.InsertFailed)
                    {
                        return Ok(new { Status = ResponseStatus.Fail, Message = "Task Details insert Failed!", Data = data });
                    }
                    else if (data.Result == (int)CRUDResponse.UpdateSuccess)
                    {
                        return Ok(new { Status = ResponseStatus.Success, Message = "Task Details update successfully!", Data = data });
                    }
                    else if (data.Result == (int)CRUDResponse.UpdateFailed)
                    {
                        return Ok(new { Status = ResponseStatus.Success, Message = "Task Details update Failed!", Data = data });
                    }
                    else
                    {
                        return Ok(new { Status = ResponseStatus.Fail, Message = "Task Details update Failed!", });
                    }

                }
                else
                {

                    return Ok(new { Status = ResponseStatus.Fail, Message = "Task Details Somthing Wrong!!", });
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult<IActionResult>(Ok(new { Status = ResponseStatus.Error, Message = ex }));
            }
           
        }



        [HttpGet]
        public async Task<IActionResult> GetTask(int TaskId)
        {
            try
            {
                var returnedData = _taskService.GetTask(TaskId);

                if (returnedData.Count > 0)
                {
                    return await Task.FromResult<IActionResult>(Ok(new { Status = ResponseStatus.Success, Message = "GetAllTask returned successfully!", data = returnedData }));

                }
                return await Task.FromResult<IActionResult>(Ok(new { Status = ResponseStatus.Fail, Message = " Something went Wrong!" }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult<IActionResult>(Ok(new { Status = ResponseStatus.Error, Message = ex }));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int TaskId)
        {
            try
            {
                return Ok(new { Status = ResponseStatus.Success, Message = "DeletedTask successfully!", Data = _taskService.DeleteTask(TaskId)});
               
            }
            catch (Exception ex)
            {
                return await Task.FromResult<IActionResult>(Ok(new { Status = ResponseStatus.Fail, Message = ex }));
            }
        }


    }
}
