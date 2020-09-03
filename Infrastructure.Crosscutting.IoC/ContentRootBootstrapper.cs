using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Service.Services;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Crosscutting.IoC
{
    public static class ContentRootBootstrapper
    {
        public static void RegisterDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if(GlobalConstants.isOnline)
                services.AddDbContext<HortaContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AzureConnection")));
            else
                services.AddDbContext<HortaContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<IPerfilService, PerfilService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
        }
    }
}
