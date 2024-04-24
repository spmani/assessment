using HCA.Data.Entities;
using HCA.Model;
using HCA.Repository.IHCARepo;
using HCA.Utitlities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;

namespace HCA.Repository.HCARepo
{
    internal class TaskRepo : ITaskRepo
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskRepo> _logger;
        public TaskRepo( AppDbContext context, ILogger<TaskRepo> logger)
        {
            _context = context;
            _logger = logger;
           
        }

        public List<TaskDetailsResponse> GetAllTask()
        {
            List<TaskDetailsResponse> resultData = new List<TaskDetailsResponse>();
            try
            {
                var data = _context.TaskDetails
                                     .Include(res => res.objTaskTag)
                                     .Include(res => res.EmployeeDetails)
                                     .Include(res => res.Status)
                                     .ToList();
                foreach (var item in data)
                {
                    TaskDetailsResponse task = new TaskDetailsResponse
                    {
                        Id = item.Id,
                        TaskName = item.TaskName,
                        TagsArray = [],
                        // Assuming objTaskTag is a collection
                        Duedate = item.Due_date,
                        assignedTo = item.EmployeeDetails?.Name, // Assuming HCA_EmployeeDetails is a single entity
                        status = item.Status?.StatusDescription // Assuming Status is a single entity
                    };
                    if (item.objTaskTag != null && item.objTaskTag.Count > 0)
                    {
                        foreach (var tag in item.objTaskTag)
                        {
                            var tasgData = new tags
                            {
                                Id = tag.Id,
                                TaskId = tag.TaskId,
                                TagsName = tag.TagsName,

                            };
                            task.TagsArray.Add(tasgData);
                        }
                    }
                    resultData.Add(task);
                }

            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Task Repository GetAllTask error:{ex.Message}");
                return null;
            }




            return resultData;
        }
        public TaskResponse InsertUpdateTask(TaskRequest objTaskRequest)
        {
            TaskResponse objTaskResponse = new TaskResponse();

            if (objTaskRequest.TaskData.Count > 0)
            {
                try
                {
                    List<TaskDetails> tasks = new List<TaskDetails>();
                    foreach (var value in objTaskRequest.TaskData)
                    {
                        if (value.Id.Equals(0))
                        {
                            TaskDetails data = new TaskDetails
                            {
                                TaskName = value.TaskName,
                                Due_date = value.Due_date,
                                EmployeeDetails = _context.HCA_EmployeeDetails.Find(value.EmployeeId),
                                Status = _context.HCA_Status.Find(value.StatusId),
                                IsActive = true,
                                Createdby = value.Createdby,
                                CreateDate = DateTime.Now,
                                objTaskActivity = new List<TaskActivityMapping>(),
                                objTaskTag = new List<TaskTagMapping>()
                            };
                            foreach (var item in value.TaskActivityarray)
                            {
                                TaskActivityMapping objTaskActivityMapping = new TaskActivityMapping
                                {
                                    ActivityDate = item.ActivityDate,
                                    ActivityDescription = item.Activitydescription,
                                    DoneBy =  item.DoneBy,
                                    IsActive = true,
                                    CreatedBy = item.Createdby,
                                    CreateDate = DateTime.Now,
                                };
                                data.objTaskActivity.Add(objTaskActivityMapping);
                            }

                            tasks.Add(data);

                            foreach (var item in value.TaskTagarray)
                            {
                                TaskTagMapping objTaskTagMapping = new TaskTagMapping
                                {
                                    TagsName = item.TagsName,
                                    IsActive = true,
                                    Createdby = item.Createdby,
                                    CreateDate = DateTime.Now,
                                };
                                data.objTaskTag.Add(objTaskTagMapping);
                            }

                            tasks.Add(data);
                            _context.TaskDetails.AddRange(tasks);
                            _context.SaveChanges();
                            objTaskResponse.Result = (int?)CRUDResponse.InsertSuccess;
                        }
                        else
                        {
                            TaskDetails data = _context.TaskDetails.Include(td => td.objTaskActivity)
                                        .Include(td => td.objTaskTag)
                                        .FirstOrDefault(td => td.Id == value.Id);

                            if (data != null)
                            {
                                data.TaskName = value.TaskName;
                                data.Due_date = value.Due_date;
                                data.EmployeeDetails = _context.HCA_EmployeeDetails.Find(value.EmployeeId);
                                data.Status = _context.HCA_Status.Find(value.StatusId);
                                data.IsActive = true;
                                data.Createdby = value.Createdby;
                                data.CreateDate = DateTime.Now;
                                foreach (var item in value.TaskActivityarray)
                                {
                                    TaskActivityMapping existingActivity = data.objTaskActivity.FirstOrDefault(a => a.Id == item.Id);
                                    if (existingActivity != null)
                                    {
                                        // Update existing activity
                                        existingActivity.ActivityDate = item.ActivityDate;
                                        existingActivity.ActivityDescription = item.Activitydescription;
                                        existingActivity.DoneBy = item.DoneBy;
                                        existingActivity.IsActive = true;
                                        existingActivity.CreatedBy = item.Createdby;
                                        existingActivity.CreateDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        TaskActivityMapping newActivity = new TaskActivityMapping
                                        {
                                            ActivityDate = item.ActivityDate,
                                            ActivityDescription=item.Activitydescription,
                                            DoneBy = item.DoneBy,
                                            IsActive = true,
                                             CreatedBy=item.Createdby,
                                            CreateDate = DateTime.Now
                                        };
                                        data.objTaskActivity.Add(newActivity);
                                    }
                                }

                                foreach (var item in value.TaskTagarray)
                                {
                                    TaskTagMapping existingTag = data.objTaskTag.FirstOrDefault(t => t.Id == item.Id);
                                    if (existingTag != null)
                                    {
                                        existingTag.TagsName = item.TagsName;
                                        existingTag.IsActive = true;
                                        existingTag.Createdby = item.Createdby;
                                        existingTag.CreateDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        TaskTagMapping newTag = new TaskTagMapping
                                        {
                                            TagsName = item.TagsName,
                                            IsActive = true,
                                            Createdby = item.Createdby,
                                            CreateDate = DateTime.Now
                                        };
                                        data.objTaskTag.Add(newTag);
                                    }
                                }

                                _context.SaveChanges();
                                objTaskResponse.Result = (int?)CRUDResponse.UpdateSuccess;
                            }
                            

                        }
                       
                    }

                    //_ = _notificationRepo.ConstructEmail(new Notification { To= "ponnusamy.r@altrockstech.com" }).Result;

                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Task Repository InsertUpdateTask error:{ex.Message}");
                    return null;
                }
            }

            return objTaskResponse;
        }


        public List<TaskDetailsResponse> GetTask(int TaskId)
        {
            List<TaskDetailsResponse> resultData = new List<TaskDetailsResponse>();
            try
            {
                var data = _context.TaskDetails
              .Include(a => a.objTaskTag)
              .Include(b => b.EmployeeDetails)
              .Include(c => c.Status)
              .Include(d => d.objTaskActivity)
              .FirstOrDefault(e => e.Id == TaskId);

                if (data != null)
                {
                    TaskDetailsResponse task = new TaskDetailsResponse
                    {
                        Id = data.Id,
                        TaskName = data.TaskName,
                        TagsArray = data.objTaskTag?.Select(tag => new tags
                        {
                            Id = tag.Id,
                            TaskId = tag.TaskId,
                            TagsName = tag.TagsName
                        }).ToList() ?? new List<tags>(),
                        Duedate = data.Due_date,
                        assignedTo = data.EmployeeDetails?.Name,
                        status = data.Status?.StatusDescription,
                        ActivityArray = data.objTaskActivity?.Select(activity => new Activity
                        {
                            Id = activity.Id,
                            TaskId = activity.TaskId,
                            ActivityDate = activity.ActivityDate,
                            Activitydescription = activity.ActivityDescription,
                            //DoneBy = activity.DoneByEmployee.Id,
                            // Add more properties if needed
                        }).ToList() ?? new List<Activity>()
                    };

                    resultData.Add(task);
                }


            }

            catch (Exception ex)
            {
               
                _logger.LogError(ex, $"Task Repostory GetTask error:{ex.Message}");
            }
            return resultData;
          
        }

        public long DeleteTask(int TaskId)
        {
            try
            {
                var Deletetag = _context.TaskTagMapping.Where(w => w.TaskId.Equals(TaskId)).ToList();
                foreach (var value in Deletetag)
                {
                    try
                    {
                        TaskTagMapping obj = _context.TaskTagMapping.Where(w => w.Id.Equals(value.Id)).FirstOrDefault();
                        _context.TaskTagMapping.Remove(obj);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _logger.LogError(ex, $"Task Repostory TaskTagMapping error:{ex.Message}");
                    }

                }

                var DeleteActivit = _context.TaskActivityMapping.Where(w => w.TaskId.Equals(TaskId)).ToList();
                foreach (var value in DeleteActivit)
                {
                    try
                    {
                        TaskActivityMapping obj = _context.TaskActivityMapping.Where(w => w.Id.Equals(value.Id)).FirstOrDefault();
                        _context.TaskActivityMapping.Remove(obj);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _logger.LogError(ex, $"Task Repostory TaskActivityMapping error:{ex.Message}");
                    }

                }

                if (TaskId != null)
                {
                    TaskDetails obj = _context.TaskDetails.Where(w => w.Id.Equals(TaskId)).FirstOrDefault();
                    _context.TaskDetails.Remove(obj);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                _logger.LogError(ex, $"Task Repostory DeleteTask error:{ex.Message}");
                return -1;
            }

            return 1;
        }






    }
}
