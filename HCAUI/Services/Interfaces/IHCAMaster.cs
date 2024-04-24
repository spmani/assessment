using HCA.Model;

namespace HCAUI.Services.Interfaces
{
    public interface IHCAMaster
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployee();
    }
}
