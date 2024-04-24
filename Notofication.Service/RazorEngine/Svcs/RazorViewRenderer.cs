using Microsoft.Extensions.Logging;
using Notofication.Service.RazorEngine.ISvcs;
using Razor.Templating.Core;

namespace Notofication.Service.RazorEngine.Svcs
{
    public class RazorViewRenderer : IRazorViewRenderer
    {
        private readonly ILogger<RazorViewRenderer> _log;

        public RazorViewRenderer(ILogger<RazorViewRenderer> log)
        {
            _log = log;
        }

        // Renders the Razor View(.cshtml) Without Layout to String with RenderPartialViewAsync
        public async Task<string> RenderPartialViewAsync<T>(string viewPath, T model)
        {
            try
            {
                return await RazorTemplateEngine.RenderPartialAsync(viewPath, model);
            }
            catch (Exception ex)
            {
                _log.LogError("Something went wrong: {ex}", ex);
                throw;
            }
        }

        public async Task<string> RenderViewAsync<T>(string viewPath, T model)
        {
            try
            {
                return await RazorTemplateEngine.RenderAsync(viewPath, model);
            }
            catch (Exception ex)
            {
                _log.LogError("Something went wrong: {ex}", ex);
                throw;
            }
        }
    }

}
