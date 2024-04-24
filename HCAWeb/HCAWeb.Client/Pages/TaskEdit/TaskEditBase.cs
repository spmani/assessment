using Microsoft.AspNetCore.Components;
using static HCAWeb.Client.Pages.TaskForm.TaskFormBase;
using System.Threading.Tasks;

namespace HCAWeb.Client.Pages.TaskEdit
{
    public class TaskEditBase : ComponentBase
    {
        public List<TaskModel1> Tasks1 = new List<TaskModel1>() { new TaskModel1() };
        protected List<Activity> Activities { get; set; } = new List<Activity>();
        protected List<TaskModel> Tasks = new List<TaskModel>(); // Assuming this list contains existing tasks
        protected List<string> TagOptions = new List<string> { "household", "weekly stuff", "work", "personal" };
        protected List<string> AssignOptions = new List<string> { "bala", "john", "alice" };
        protected string ActivityDescription { get; set; }
        protected string DoneBy { get; set; }
        protected string NewTaskName { get; set; }
        protected string NewTaskTags { get; set; }
        protected string AssignTo { get; set; }
        protected DateTime NewTaskDueDate { get; set; } = DateTime.Today;
        protected TaskModel TaskToUpdate; // To store the task being updated
        protected List<string> People { get; set; } = new List<string> { "Person 1", "Person 2", "Person 3" };
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

        [Parameter]
        public string Id { get; set; }

        protected override void OnInitialized()
        {
            // Retrieve task details based on Id parameter
           
        }

        protected void UpdateTask()
        {
            TaskToUpdate = Tasks.FirstOrDefault(t => t.Id.ToString() == Id);

            // Populate UI fields with task details
            if (TaskToUpdate != null)
            {
                NewTaskName = TaskToUpdate.TaskName;
                NewTaskTags = string.Join(",", TaskToUpdate.Tags);
                AssignTo = TaskToUpdate.AssignedTo;
                NewTaskDueDate = TaskToUpdate.DueDate;
            }
        }

        protected void AddTask()
        {
            var data = new TaskModel1();
            Tasks1.Add(data);
        }

        protected void AddActivity()
        {
            Activities.Add(new Activity());
        }

        protected void DeleteTask(TaskModel1 task)
        {
            Tasks1.Remove(task);
        }

        protected void DeleteActivity(Activity activity)
        {
            Activities.Remove(activity);
        }
    }
}
public class Activity
{
    public DateTime ActivityDate { get; set; }
    public string ActivityDescription { get; set; }
    public string DoneBy { get; set; }
}
