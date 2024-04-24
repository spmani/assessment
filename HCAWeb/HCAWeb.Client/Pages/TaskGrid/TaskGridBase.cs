using HCAWeb.Client.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.Net.Http.Headers;

namespace HCAWeb.Client.Pages.TaskGrid
{
    public class TaskGridBase : ComponentBase
    {
        protected List<TaskModel> Tasks = new List<TaskModel>();
        protected List<string> TagOptions = new List<string> { "household", "weekly stuff", "work", "personal" };
        protected List<string> AssignOptions = new List<string> { "bala", "john", "alice" };
        protected string NewTaskName { get; set; }
        protected string NewTaskTags { get; set; }
        protected string AssignTo { get; set; }
        protected DateTime NewTaskDueDate { get; set; } = DateTime.Today;

        [Inject]
        public ITaskService taskService { get; set; }



        // protected void NavigateToAddTask()
        // {
        //     NavigationManager.NavigateTo("/AddTask");
        // }

        protected override void OnInitialized()
        {
            LoadSampleTasks();
        }

        public class TaskModel
        {
            public int Id { get; set; }
            public string TaskName { get; set; }
            public List<string> Tags { get; set; }
            public DateTime DueDate { get; set; }
            public string Color { get; set; }
            public string AssignedTo { get; set; }
            public string Status { get; set; }

            public TaskModel()
            {
                Tags = new List<string>();
            }
        }

        // protected void UpdateTask(TaskModel task)
        // {
        //     NavigationManager.NavigateTo("EditTask");
        // }

        // protected void DeleteTask(TaskModel task)
        // {
        //     NavigationManager.NavigateTo("DeleteTask");
        // }

        protected void LoadSampleTasks()
        {
            var data = taskService.GetTasks();

            Tasks = new List<TaskModel>();

            foreach (var taskData in data)
            {
                var tags = taskData.Tags.Split(',').ToList(); // Split tags string into a list

                // Parse due date string to DateTime
                DateTime dueDate = taskData.Duedate.GetValueOrDefault();
                if (taskData.Duedate.HasValue)
                {
                    dueDate = taskData.Duedate.Value;
                }

                // Create a new TaskModel object and add it to the Tasks list
                Tasks.Add(new TaskModel
                {
                    Id = taskData.Id,
                    TaskName = taskData.TaskName,
                    Tags = tags,
                    DueDate = dueDate,
                    Color = "Blue",
                    AssignedTo = taskData.assignedTo,
                    Status = taskData.status
                });
            }
        }

        protected async Task AddTask()
        {
            Tasks.Add(new TaskModel
            {
                TaskName = NewTaskName,
                Tags = !string.IsNullOrEmpty(NewTaskTags) ? NewTaskTags.Split(',').ToList() : new List<string>(),
                DueDate = NewTaskDueDate,
                Color = "", // Assign color as needed
                AssignedTo = AssignTo,
                Status = "PENDING" // Set initial status
            });

        }

    }
}
