using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleDAL;
using SampleDAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleService
{
    public static class DependencyInjection
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            RegisterDataBase(services, configuration);

            RegisterDataServices(services);
            RegisterDomainServices(services);
        }

        private static void RegisterDataBase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SampleContext")));
        }

        private static void RegisterDataServices(IServiceCollection services)
        {
            services.RegisterTypes(InversionControl.GetDataTypes());
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.RegisterTypes(InversionControl.GetDomainTypes());
        }

        private static void RegisterTypes(this IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var item in types)
            {
                services.AddTransient(item.Key, item.Value);
            }
        }

    }
}
