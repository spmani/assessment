using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Model
{
    public class ResponseModel<T>
    {
        public int status {  get; set; }

        public string? message { get; set; }
        public IEnumerable<T>? data { get; set; }

        
    }
}
