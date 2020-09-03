using System;
using Infrastructure.Crosscutting.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedeHortaRayanRalile.Areas.Identity.Data;
using RedeHortaRayanRalile.Data;

[assembly: HostingStartup(typeof(RedeHortaRayanRalile.Areas.Identity.IdentityHostingStartup))]
namespace RedeHortaRayanRalile.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            if (GlobalConstants.isOnline)
                builder.ConfigureServices((context, services) =>
                {
                    services.AddDbContext<RedeHortaIdentityContext>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("AzureIdentityDB")));

                    services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = false)
                        .AddEntityFrameworkStores<RedeHortaIdentityContext>();
                });
            else
                builder.ConfigureServices((context, services) =>
                {
                    services.AddDbContext<RedeHortaIdentityContext>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("RedeHortaIdentityContextConnection")));

                    services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = false)
                        .AddEntityFrameworkStores<RedeHortaIdentityContext>();
                });
        }
    }
}