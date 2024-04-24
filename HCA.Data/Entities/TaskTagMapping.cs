using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Data.Entities
{
    public class TaskTagMapping
    {
        public int Id { get; set; }
        public int TaskId { get; set; }

        public string? TagsName { get; set; }
        public bool IsActive { get; set; }

        public DateTime? CreateDate { get; set; }
        public string? Createdby { get; set; }
        public DateTime?  ModifiedDate { get; set; }
        public string? Modifiedby { get; set; }

        [ForeignKey("TaskId")]
        public  TaskDetails? TaskDetails { get; set; }
       
    }
}
