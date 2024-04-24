using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notofication.Service.Email.Models
{
    public class EmailRequest
    {
        public string To { get; set; }      
        public string Subject { get; set; }
        public IEnumerable<IFormFile> Attachment { get; set; }
        public string Body { get; set; }
    }

    public class EmailResponse
    {
        public bool Status { get; set; }
        public string Response { get; set; }
    }
}
