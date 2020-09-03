using Domain.Service.Services;
using Infrastructure.Crosscutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedeHortaRayanRalile.ApiServices.Implementations;
using RedeHortaRayanRalile.ApiServices.Interfaces;
using RedeHortaRayanRalile.Files.Implementations;
using RedeHortaRayanRalile.Files.Interfaces;

namespace RedeHortaRayanRalile
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            if (GlobalConstants.isOnline)
            {
                services.AddScoped<IPerfilApiService, PerfilApiService>();
                services.AddScoped<IPostApiServices, PostApiService>();
            }
            else
            {
                services.AddScoped<IPerfilApiService, PerfilApiServiceOFFLINE>();
                services.AddScoped<IPostApiServices, PostApiServiceOFFLINE>();
            }
            services.AddScoped<IPerfilFileUploader, PerfilFileUploader>();
            services.AddScoped<IPostFileUploader, PostFileUploader>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
