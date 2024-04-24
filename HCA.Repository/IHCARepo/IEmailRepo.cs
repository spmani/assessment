using HCA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Repository.IHCARepo
{
    public interface IEmailRepo
    {
        public bool SyncEmailTrigger();
    }
}
