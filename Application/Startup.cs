using Application.Models;
using Application.Services;
using Application.Services.Abstract;
using Application.Settings;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Startup
    {
        public static void ApplicationInit(this IServiceCollection services, IConfiguration config)
        {
            AddServices(services);
            AddSettings(services, config);

        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IValidator<ParserRequestDto>, ParserValidator>();
            services.AddScoped<IParserService, ParserService>();
        }

        public static void AddSettings(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("postgre");

            services.Configure<DatabaseOptions>(options =>
            {
                options.ConnectionString = connectionString;
            });

        }

    }
}
