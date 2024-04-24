using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notofication.Service.RazorEngine.ISvcs
{
    public interface IRazorViewRenderer
    {
        Task<string> RenderPartialViewAsync<T>(string viewPath, T model);
        Task<string> RenderViewAsync<T>(string viewPath, T model);
    }
}
