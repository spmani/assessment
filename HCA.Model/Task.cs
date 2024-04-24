using HCA.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Model
{
    public class TaskRequest
    {
        public List<TaskDetailsRequest> TaskData { get; set; } = null;
    }


    public class TaskDetailsRequest
    {
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public DateTime Due_date { get; set; }
        public int EmployeeId { get; set; }
        public int StatusId { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Createdby { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<TaskActivityMappingRequest> TaskActivityarray { get; set; } = null;
        public List<TaskTagMappingRequest> TaskTagarray { get; set; } = null;
    }


    public class TaskActivityMappingRequest
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime? ActivityDate { get; set; }
        public int DoneBy { get; set; }
        public string? Activitydescription { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Createdby { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Modifiedby { get; set; }
    }


    public class TaskTagMappingRequest
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? TagsName { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Createdby { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Modifiedby { get; set; }

       
    }


    public class TaskResponse
    {
        public int? Result { get; set; }

    }


    public class TaskDetailsResponse
    {
      public int Id { get; set; }
        public string? TaskName { get; set; } = string.Empty;
        public string? Tags { get; set; } = string.Empty;
        public DateTime? Duedate { get; set; }
        public string? assignedTo { get; set; } = string.Empty;
        public string? status { get; set; } = string.Empty;
        public List<tags>? TagsArray { get; set; } = null;
        public List<Activity>? ActivityArray { get; set; } = null;
    }

    public class tags
    {
        public int Id { get; set; }
        public int TaskId { get; set; }

        public string? TagsName { get; set; }
    }

    public class Activity
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime? ActivityDate { get; set; }
        public int DoneBy { get; set; }
        public string? Activitydescription { get; set; }

    }

}
