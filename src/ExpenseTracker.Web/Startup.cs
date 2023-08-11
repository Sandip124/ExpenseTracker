using System;
using System.Text;
using AspNetCoreHero.ToastNotification;
using ExpenseTracker.Infrastructure.Configurations;
using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Middleware;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;

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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnection()).EnableDetailedErrors();
            });

            services.AddCookiePolicyConfiguration();

            services.Configure<CookieTempDataProviderOptions>(options => { options.Cookie.IsEssential = true; });
            
            services.AddCookieAuthenticationConfiguration(Configuration.GetSecret());

            services.AddControllersWithViews();

            services.AddSessionConfiguration();

            services.AddMvc().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                }).AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddHttpContextAccessor();
            services.AddScoped<DbContext, AppDbContext>();
            services.UseExpenseTracker();
            
            services.AddNotyf(config=> { config.DurationInSeconds = 10;config.IsDismissable = true;config.Position = NotyfPosition.BottomCenter; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider,ILoggerFactory loggerFactory,AppDbContext appDbContext,ISeedingService seedingService)
        {
            ServiceActivator.Configure(app.ApplicationServices);
            
            var hasDb = appDbContext.Database.CanConnect();
            appDbContext.Database.Migrate();
            if (!hasDb)
            {
                try
                {
                    var success = seedingService.InitialSeed(appDbContext).Result;
                }
                catch (Exception e)
                {
                    appDbContext.Database.EnsureDeleted();
                    throw e;
                }
                
            }
            
            loggerFactory.AddSerilog();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/Error/Index");
            }
            else
            {
                app.UseExceptionHandler("/Errors/Index");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            appDbContext.Database.Migrate();
            
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseWorkspaceMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
            });
        }
    }
}