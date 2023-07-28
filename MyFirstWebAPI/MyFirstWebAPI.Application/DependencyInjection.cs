using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFirstWebAPI.Application.CountryManagement.Queries;
using MyFirstWebAPI.Application.CountryManagement.Services;

namespace MyFirstWebAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ICountryQueries, CountryQueries>();
            services.AddTransient<ICountryService, CountryService>();

            return services;
        }
    }
}
