using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Data.Entities
{
    public class TaskDetails
    {
        public int Id { get; set; }
        public string? TaskName { get; set; }

        public DateTime Due_date { get; set; }

        public int EmployeeId { get; set; }

        public int StatusId { get; set; }
        public bool IsActive { get; set; }

        public DateTime? CreateDate { get; set; }
        public string? Createdby { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Modifiedby { get; set; }


        public bool Emailtriggered { get; set; }=false;
        [ForeignKey("StatusId")]
        public required HCA_Status Status { get; set; }

        [ForeignKey("EmployeeId")]
        public required HCA_EmployeeDetails EmployeeDetails { get; set; }

        public virtual IList<TaskActivityMapping>? objTaskActivity { get; set; }
        public virtual IList<TaskTagMapping>? objTaskTag { get; set; }
    }
}
