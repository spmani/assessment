using HCA.Model;
using HCA.Utitlities;
using HCA.Model.HCAUI;
using HCAWeb.Client.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using CurrieTechnologies.Razor.SweetAlert2;
using HCAWeb.Client.Services;


namespace HCAWeb.Client.Pages.TaskForm
{
    public class TaskFormBase:ComponentBase
    {
        [Inject]
        protected SweetAlertService sweetAlertService { get; set; }

        [Inject]
        public ITaskService taskService { get; set; }
        
        [Inject]
        public NavigationManager navigationManager { get; set; }

      
        public List<TaskModel1> Tasks1 = new List<TaskModel1>() { new TaskModel1() };

       
        protected List<string> TagOptions = new List<string> { "household", "weekly stuff", "work", "personal" };
       
        protected string NewTaskName { get; set; }
        protected string NewTaskTags { get; set; }
        protected int AssignTo { get; set; }
        protected DateTime NewTaskDueDate { get; set; } = DateTime.Today;
        protected DateTime ActivityDate { get; set; } = DateTime.Today;
        protected string ActivityDescription { get; set; }
        protected string DoneBy { get; set; }
        protected List<string> People { get; set; } = new List<string> { "Person 1", "Person 2", "Person 3" };

        protected IEnumerable<EmployeeDto> Employees { get; set; } 

        protected List<string> SelectedTags { get; set; } = new List<string>();
        protected List<Activity> Activities { get; set; } = new List<Activity>();
        protected int Count { get; set; }
        [Inject]
        public IHCAMaster hCAMaster { get; set; }

        public class TaskModel1
        {
            public string TaskName { get; set; }
            //public string TaskTags { get; set; }
            public int AssignTo { get; set; } = 0;
            public DateTime NewTaskDueDate { get; set; }
            public List<Activity> Activities { get; set; } = new List<Activity>();

            protected string _taskTagsAsString;
            public string TaskTagsAsString
            {
                get => _taskTagsAsString;
                set
                {
                    _taskTagsAsString = value;
                    TaskTags = value?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select((tag, index) => $"{index}.{tag.Trim()}").ToList() ?? new List<string>();
                }
            }

            public List<string> TaskTags { get; protected set; } = new List<string>();
        }
        
        public class TaskActivityModel
        {
            public DateTime ActivityDate { get; set; }
            public int DoneBy { get; set; }
            public string ActivityDescription { get; set; }
        }



        protected override async Task OnInitializedAsync()
        {

            Employees = await hCAMaster.GetAllEmployee();

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

        public class Activity
        {
            public DateTime ActivityDate { get; set; }
            public string ActivityDescription { get; set; }
            public int DoneBy { get; set; }
        }



        protected void ToggleTag(string tag)
        {
            if (SelectedTags.Contains(tag))
                SelectedTags.Remove(tag);
            else
                SelectedTags.Add(tag);
        }


        protected List<string> GetTagsFromString(string tagsString)
        {
            if (string.IsNullOrWhiteSpace(tagsString))
                return new List<string>();

            return tagsString.Split(',').Select(tag => tag.Trim()).ToList();
        }


      


        public async Task SaveTask()
        {
            bool isVaild = true;
            // Create a list to store activity mappings
            var activityMappings = new List<TaskActivityMappingRequest>();

            foreach (var activity in Activities)
            {
                activityMappings.Add(new TaskActivityMappingRequest
                {
                    ActivityDate = activity.ActivityDate,
                    DoneBy = activity.DoneBy,
                    Activitydescription = activity.ActivityDescription,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    Createdby = "system"
                });
            }

            // Create a list to store tag mappings
            var tagMappings = new List<TaskTagMappingRequest>();

            var taskDetailsList = new List<TaskDetailsRequest>(); // List to store all task details

            foreach (var task in Tasks1)
            {
                if (string.IsNullOrEmpty(task.AssignTo.ToString()))
                {
                    isVaild = false;
                }
                tagMappings.Add(new TaskTagMappingRequest
                {
                    TagsName = task.TaskTagsAsString,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    Createdby = "system"
                });

                // Create TaskDetailsRequest object for each task
                var taskDetails = new TaskDetailsRequest
                {
                    TaskName = task.TaskName,
                    Due_date = task.NewTaskDueDate,
                    EmployeeId = task.AssignTo,
                    StatusId = 1,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    Createdby = "system",
                    TaskActivityarray = activityMappings,
                    TaskTagarray = tagMappings
                };

                taskDetailsList.Add(taskDetails); // Add task details to the list
            }

            // Create TaskRequest object
            var taskRequest = new TaskRequest
            {
                TaskData = taskDetailsList // Assign the list of task details to TaskData
            };

            if (taskRequest != null)
            {
                if (isVaild)
                {
                    var response = taskService.AddTask(taskRequest); // Assuming AddTask is an asynchronous method

                    if (response != null)
                    {
                        if (response.status == 0)
                        {
                            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
                            {
                                Title = response.message.ToString(),
                                Text = "",
                                Icon = SweetAlertIcon.Success,
                                ShowCancelButton = false,
                                ConfirmButtonText = "OK",
                                CancelButtonText = "Cancel",


                            });
                            if (!string.IsNullOrEmpty(result.Value))
                            {
                                navigationManager.NavigateTo("../", true);
                            }
                        }
                        else if (response.status == -1)
                        {
                            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
                            {
                                Title = response.message.ToString(),
                                Text = "Please try after some time!",
                                Icon = SweetAlertIcon.Warning,
                                ShowCancelButton = true,
                                ConfirmButtonText = "OK",
                                CancelButtonText = "Cancel",


                            });
                            if (!string.IsNullOrEmpty(result.Value))
                            {
                                navigationManager.NavigateTo("../", true);
                            }
                        }
                        else
                        {
                            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
                            {
                                Title = response.message.ToString(),
                                Text = "Please try after some time!",
                                Icon = SweetAlertIcon.Info,
                                ShowCancelButton = true,
                                ConfirmButtonText = "OK",
                                CancelButtonText = "Cancel",
                            });
                            if (!string.IsNullOrEmpty(result.Value))
                            {
                                navigationManager.NavigateTo("../", true);
                            }
                        }
                    }

                }
                else
                {
                    var result = await sweetAlertService.FireAsync(new SweetAlertOptions
                    {
                        Title = "Please fill All required field",
                        Text = "",
                        Icon = SweetAlertIcon.Info,
                        ShowCancelButton = false,
                        ConfirmButtonText = "OK"
                        
                    });
                }
                 }
        }

    }
}
