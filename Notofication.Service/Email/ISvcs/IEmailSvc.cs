using Notofication.Service.Email.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notofication.Service.Email.ISvcs
{
    public interface IEmailSvc
    {
        Task<string> ConstructMailAsync<TModel>(TModel model);
        Task<EmailResponse> SendEmailAsync(EmailRequest email);
    }
}
