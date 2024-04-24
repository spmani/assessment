using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Data.Entities
{
    public class HCA_Status
    {
        public int Id { get; set; }
        public string? StatusDescription { get; set; }
        public bool IsActive { get; set; }

        public DateTime? CreateDate { get; set; }
        public string? Createdby { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Modifiedby { get; set; }
    }
}
