using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;

namespace HCA.Repository.IHCARepo
{
    public interface INotificationRepo
    {
        Task<bool> ConstructEmail(Notification notification);
    }
}
