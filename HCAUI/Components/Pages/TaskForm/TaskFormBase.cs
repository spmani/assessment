using HCA.Model.HCAUI;
using Microsoft.AspNetCore.Components;

namespace HCAUI.Components.Pages.TaskForm
{
    public class TaskFormBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<TaskDto> formDetails { get; set; }
    }
}
