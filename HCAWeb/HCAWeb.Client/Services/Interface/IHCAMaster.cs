using HCA.Model;

namespace HCAWeb.Client.Services.Interface
{
    public interface IHCAMaster
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployee();

    }
}
