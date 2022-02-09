using System;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;

namespace ExpenseTracker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()  
                .Enrich.FromLogContext()  
                .WriteTo.Console(new RenderedCompactJsonFormatter())  
                .WriteTo.Debug(outputTemplate:DateTime.Now.ToString(CultureInfo.InvariantCulture))  
                .WriteTo.File("logs/log.txt",rollingInterval:RollingInterval.Day)  
                .CreateLogger();  
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel((context, serverOptions) =>
                    {
                        serverOptions.Limits.MinRequestBodyDataRate =
                            new MinDataRate(50, TimeSpan.FromSeconds(20));
                        serverOptions.Limits.MinResponseDataRate =
                            new MinDataRate(50, TimeSpan.FromSeconds(20));
                    });
                });
    }
}