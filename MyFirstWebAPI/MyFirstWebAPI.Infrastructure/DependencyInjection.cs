using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFirstWebAPI.Infrastructure.Common.Interfaces;
using MyFirstWebAPI.Infrastructure.Repositories;
using MyFirstWebAPI.Infrastructure.Services;
using System;

namespace MyFirstWebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddScoped<IDapperService, DapperService>();
            return services;
        }
    }
}
