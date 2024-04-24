using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HCA.Data.Entities
{
    public class TaskActivityMapping
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime? ActivityDate { get; set; }
        public int DoneBy { get; set; }
        public string ActivityDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        [ForeignKey("TaskId")]
        public TaskDetails? TaskDetails { get; set; }


        [ForeignKey("DoneBy")]
        public virtual HCA_EmployeeDetails DoneByEmployee { get; set; }
    }
}
