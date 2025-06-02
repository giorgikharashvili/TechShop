using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TechShop.Infrastructure.Data;
using TechShop.Infrastructure.Repositories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
