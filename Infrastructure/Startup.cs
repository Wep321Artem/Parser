using Application.Models;
using Application.RepositoriesAbstract;
using Application.Services;
using Application.Services.Abstract;
using Application.Validators;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class Startup
    {

        public static void InfrastructureInit(this IServiceCollection services)
        {
            AddServices(services);

        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IParseRepository, ParseRepository>();
        }

    }
}
