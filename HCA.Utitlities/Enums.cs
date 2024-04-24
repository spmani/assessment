using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Utitlities
{
    public enum CRUDResponse
    {
        InsertSuccess = 1,
        InsertFailed = 2,
        UpdateSuccess = 3,
        UpdateFailed = 4,
    }
    public enum ResponseStatus
    {
        Success = 0,
        Fail = -1,
        Error = 1
    }
}
