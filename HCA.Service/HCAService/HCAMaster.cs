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
    internal class HCAMaster : IHCAMaster
    {
        private readonly ILogger<HCAMaster> _logger;
        private readonly IHCAMasterRepo _taskRepo;
        public HCAMaster(ILogger<HCAMaster> logger, IHCAMasterRepo taskRepo)
        {
            _logger = logger;
            _taskRepo = taskRepo;
        }

        public List<EmployeeDto> GetAllEmployee()
        {
            try
            {
               _logger.LogInformation("Get All Employee initiated Service");
               return _taskRepo.GetAllEmployee();
            }catch(Exception ex)
            {
                _logger.LogError(ex,"Get All Emp Ser Error!");
                return new List<EmployeeDto>();
            }
        }
    }
}
