using HCA.Data.Entities;
using HCA.Model;
using HCA.Repository.IHCARepo;
using HCA.Utitlities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;
using WebAPI.Data;

namespace HCA.Repository.HCARepo
{
    public class EmailRepo:IEmailRepo
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EmailRepo> _logger;
        private readonly INotificationRepo _notificationRepo;
        public EmailRepo(AppDbContext context, ILogger<EmailRepo> logger, INotificationRepo notificationRepo)
        {
            _context = context;
            _logger = logger;
            _notificationRepo = notificationRepo;
        }
        public bool SyncEmailTrigger()
        {
            
            var data = _context.TaskDetails.Include(res => res.EmployeeDetails).ToList().Where(a => a.Emailtriggered.Equals(false));

            foreach (var value in data)
            { 
               var email= value.EmployeeDetails.Email;
                if (!string.IsNullOrEmpty(email))
                {
                    _ = _notificationRepo.ConstructEmail(new Notification { To = email }).Result;
                    value.Emailtriggered = true;
                    _context.TaskDetails.Update(value);
                    _context.SaveChanges();
                }
            }






                return true;
        }
    }
}
