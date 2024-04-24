using HCA.Repository.HCARepo;
using HCA.Repository.IHCARepo;
using Microsoft.Extensions.DependencyInjection;

namespace HCA.Repository
{
    public static class Startup
    {

        public static void HCARepository(this IServiceCollection services)
        {

            services.AddScoped<ITaskRepo, TaskRepo>();
            services.AddScoped<IHCAMasterRepo, HCAMasterRepo>();
            services.AddScoped<INotificationRepo, NotificationRepo>();
            services.AddScoped<IEmailRepo, EmailRepo>();


        }
    }
}
