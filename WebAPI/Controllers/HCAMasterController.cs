using HCA.Service.IHCAService;
using HCA.Utitlities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HCAMasterController : ControllerBase
    {
        private readonly IHCAMaster _HCAMaster;
        private readonly ILogger<IHCAMaster> _logger;
        public HCAMasterController(IHCAMaster hCAMaster, ILogger<IHCAMaster> logger) 
        {
            _HCAMaster = hCAMaster;
            _logger = logger;
        }

        [HttpGet]

        public Task<IActionResult> GetAllEmployee()
        {
            _logger.LogInformation("Get All Employee Initiated Con");

            var returnedData = _HCAMaster.GetAllEmployee();

            if (returnedData.Count>0)
            {
                _logger.LogInformation($"Get All Employee Returned data was : {0}", returnedData);
                return Task.FromResult<IActionResult>(Ok(new { Status = 0, Message = "GetAllEmployee returned successfully!", data = returnedData }));

            }else if(returnedData.Count == 0)
            {
                _logger.LogInformation($"Get All Employee Returned data was  null.");
                return Task.FromResult<IActionResult>(Ok(new { Status = 1, Message = "Get Employee Data was null!" }));

            }

            return Task.FromResult<IActionResult>(Ok(new { Status = -1, Message = " Something went Wrong!"}));

        }

    }
}
