using HCA.Model;
using HCA.Model.HCAUI;
using HCAWeb.Client.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace HCAWeb.Client.Pages
{
    public class BaseClass : ComponentBase
    {

        [Inject]
        public IHCAMaster hCAMaster { get; set; }

        public IEnumerable<EmployeeDto> Employees { get; set; }

        public IList<TaskDto> taskFormDetails = new List<TaskDto>();


        protected override async Task OnInitializedAsync()
        {
            var newTask = new TaskDto();
            taskFormDetails.Add(newTask);
            Employees = await hCAMaster.GetAllEmployee();
        }


        protected async void AddTask()
        {
            var newTask = new TaskDto();
            taskFormDetails.Add(newTask);
            Console.WriteLine("add clicked");

        }
       
    protected int currentCount = 0;

        protected void IncrementCount()
        {
            currentCount++;
        }
    



}

}
