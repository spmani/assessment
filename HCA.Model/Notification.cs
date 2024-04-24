using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class NotificationConfig
    {
        public string Body { get; set; }
        public string Subject { get; set; }
    }
    public class Notification
    {
        public string To { get; set; }
    }
}
