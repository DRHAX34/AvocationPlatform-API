using AvocationPlatform_Services.Implementations;
using AvocationPlatform_Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvocationPlatform_API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<ICandidateService, CandidateService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IOpeningService, OpeningService>();
            services.AddTransient<IRecruiterService, RecruiterService>();
            services.AddTransient<IRoomService, RoomService>();

            // cache in memory
            services.AddMemoryCache();

            // caching response for middlewares
            services.AddResponseCaching();
        }
    }
}
