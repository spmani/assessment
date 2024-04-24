using HCA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Repository.IHCARepo
{
    public interface IHCAMasterRepo
    {
        public List<EmployeeDto> GetAllEmployee();
    }
}
