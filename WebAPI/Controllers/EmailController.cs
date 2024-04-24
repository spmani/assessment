using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HCA.Service.IHCAService;
using HCA.Utitlities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]


    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;
        public EmailController(IEmailService emailService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _logger = logger;

        }

        [HttpGet]

        public Task<IActionResult> SyncEmailTrigger()
        {
            try
            {
                var returnedData = _emailService.SyncEmailTrigger();

                if (returnedData)
                {
                    return Task.FromResult<IActionResult>(Ok(new { Status = 0, Message = " Successfully!" }));
                }
                else
                {
                    return Task.FromResult<IActionResult>(Ok(new { Status = -1, Message = " Something went Wrong!" }));
                }
               

               
            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(Ok(new { Status = -1, Message = ex }));
            }
        }


    }
}
