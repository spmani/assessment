using HCA.Model;
using HCAUI.Services.Interfaces;
using Microsoft.AspNetCore.Components;



using HCAUI.Services;
using Microsoft.Extensions.Logging;

namespace HCAUI.Components.Pages
{
    public class BaseClass:ComponentBase
    {
        [Inject]
        public required HCAMaster hCAMaster { get; set; }


        public IEnumerable<EmployeeDto> Employees { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Employees = await hCAMaster.GetAllEmployee();
        }


        protected async void AddTask()
        {

            Console.WriteLine("add clicked");

        }



    }
}
