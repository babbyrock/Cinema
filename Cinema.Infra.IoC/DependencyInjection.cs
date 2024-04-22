using Cinema.Application.Interfaces;
using Cinema.Application.Mappings;
using Cinema.Application.Services;
using Cinema.Domain.Interfaces;
using Cinema.Infra.Data.Context;
using Cinema.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
                )
            );

            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<ISalaRepository, SalaRepository>();

            services.AddScoped<ISalaService, SalaService>();
            services.AddScoped<IFilmeService, FilmeService>();
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }

}
