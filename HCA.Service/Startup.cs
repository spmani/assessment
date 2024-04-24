
using HCA.Repository.HCARepo;
using HCA.Repository.IHCARepo;
using HCA.Service.HCAService;
using HCA.Service.IHCAService;
using Microsoft.Extensions.DependencyInjection;

namespace HCA.Service
{
    public static class Startup
    {
        public static void HCAService (this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IHCAMaster, HCAMaster>();            
            services.AddScoped<IEmailService, EmailService>();
        }

    }
}
