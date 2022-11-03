using Application.Core.Interfaces;
using Application.Core.Services;
using Application.Infrastructure.Data;
using Application.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure
{
    public static class StartupSetup
    {
        public static IServiceCollection AddInfrastructre(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)), ServiceLifetime.Transient);
            services.AddMemoryCache();

            services.AddTransient<IExcelDataReader, ExcelDataReaderServices>();
            services.AddTransient<IExcelRepository, ExcelRepository>();
            return services;
        }
    }
}
