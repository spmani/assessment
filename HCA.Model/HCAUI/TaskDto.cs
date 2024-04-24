using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Model.HCAUI
{
    public class TaskDto
    {
        public string taskName { get; set; } = string.Empty;

        public IEnumerable<ActivityDto> activityDetails { get; set; }

    }
    public class ActivityDto
    {
        public string ActivitDescription { get; set; } = string.Empty;
    }
}
