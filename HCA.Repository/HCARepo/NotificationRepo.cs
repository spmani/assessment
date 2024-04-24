using HCA.Repository.IHCARepo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notofication.Service.Email.ISvcs;
using Notofication.Service.Email.Models;
using Notofication.Service.Email.TModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;
using WebAPI.Data;

namespace HCA.Repository.HCARepo
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly ILogger<NotificationRepo> _log;
        private readonly AppDbContext _context;
        private readonly IEmailSvc _emailSvc;
        private readonly IConfiguration _config;


        public NotificationRepo(AppDbContext context,  IEmailSvc emailSvc, ILogger<NotificationRepo> log, IConfiguration config)
        {
            _context = context;
            _emailSvc = emailSvc;
            _log = log;
            _config = config;
        }

       

        public async Task<T> ExpenseMail<T>() where T : class
        {          
            if (typeof(T) == typeof(NewTask))
            {
                var newtaskmail = new NewTask
                {
                    UserName = "Sir !!!!",
                };

                return newtaskmail as T;
            }
            return null;
        }


        public async Task<bool> ConstructEmail(Notification notification)
        {
            try
            {
                var notificationMail = new NotificationConfig();

                // Assuming ExpenseMail<T> returns a NewTask object
                var newTaskMail = await ExpenseMail<NewTask>();

                // Construct email body
                notificationMail.Body = await _emailSvc.ConstructMailAsync(newTaskMail);               


                var data = new EmailRequest
                {
                    To = notification.To,                    
                    Subject = "New Task Added",
                    Body = notificationMail.Body,
                };

                var sendNotification = await _emailSvc.SendEmailAsync(data);

                // Return true to indicate successful execution
                return true;
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return false to indicate failure
                return false;
            }
        }


    }

}
