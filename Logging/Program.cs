using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Original
            //CreateHostBuilder(args).Build().Run();

            //Dividing above steps into two steps
            var host = CreateHostBuilder(args).Build();
            //Asked the dependency injection system for a service ILogger.
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("The application has started");
            host.Run();
        }

        //logging gets configured in CreateDefaultBuilder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    //to clear what microsoft did
                    //to clear whichever is going to get the logging
                    logging.ClearProviders();
                    //add configuration => how to configure our loggers, what's listening, who's listening
                    //load logging from appsettings.json file
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    //Add where we are going to logging
                    //logging.AddDebug();
                    logging.AddConsole();
                    //Eventsource, EventLog, TraceSource, AzureAppServicesFile, AzureAppServicesBlob, ApplicationInsights - in these we can also store
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
