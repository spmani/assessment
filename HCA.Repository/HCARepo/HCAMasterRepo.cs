using HCA.Model;
using HCA.Repository.IHCARepo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using HCA.Utitlities;

namespace HCA.Repository.HCARepo
{
  
    

    internal class HCAMasterRepo : IHCAMasterRepo
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HCAMasterRepo> _logger;

        public HCAMasterRepo( AppDbContext context, ILogger<HCAMasterRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<EmployeeDto> GetAllEmployee()
        {
            try
            {
                _logger.LogInformation("Get All Employee initiated Repo");
              var returnedData =_context.HCA_EmployeeDetails.Select(res => new EmployeeDto
                {
                    ID = res.Id,
                    Name = res.Name
                }).ToList();
                _logger.LogInformation($"Get All Employee returned data Repo was {0}",returnedData);
                return returnedData;

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Get All employee error in repo");
                return new List<EmployeeDto>(); 
            }
        }
    }
}
