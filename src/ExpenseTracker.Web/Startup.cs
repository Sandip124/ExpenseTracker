using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExpenseTracker.Web
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
            services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ExpenseTracker.Core")));

            RegisterElements(services);
            services.AddControllersWithViews();

            services.AddMvc().AddRazorRuntimeCompilation();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private void RegisterElements(IServiceCollection services)
        {
            RegisterServices(services);
            RegisterRepos(services);
            RegisterHelpers(services);
            RegisterLibraries(services);

        }
        private void RegisterServices(IServiceCollection services)
        {
            RegisterCoreServices(services);
        }
        private void RegisterRepos(IServiceCollection services)
        {
          
            RegisterCoreRepos(services);
        }
        private void RegisterCoreMakers(IServiceCollection services)
        {
        }
        private void RegisterHelpers(IServiceCollection services)
        {
        }
        private void RegisterLibraries(IServiceCollection services)
        {

        }
        private void RegisterCoreServices(IServiceCollection services)
        {
            
        }
        private void RegisterCoreRepos(IServiceCollection services)
        {
           
        }
    }
}