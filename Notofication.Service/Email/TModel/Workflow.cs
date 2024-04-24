using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notofication.Service.Email.TModel
{

    public class WorkFlowApprovalMail
    {
        public string Module { get; set; }
        public string ApproverName { get; set; }
        public string OwnerName { get; set; }
        public string Url { get; set; }
    }

 
    public class NewTask
    {
        public string UserName { get; set; }
       
    }

    

}
