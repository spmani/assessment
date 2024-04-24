using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notofication.Service.Email.ISvcs;
using Notofication.Service.Email.Svcs;
using Notofication.Service.RazorEngine.ISvcs;
using Notofication.Service.RazorEngine.Svcs;

namespace Notofication.Service
{
    public static class Startup
    {
        public static void NotificationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddScoped<IEmailSvc, EmailSvc>();
            services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();
            services.AddRazorTemplating();
        }
    }

}
