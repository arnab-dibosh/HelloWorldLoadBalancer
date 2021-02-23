using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Logging.EventLog;
using NLog.Web;

namespace TestHelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.ClearProviders();
                     logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                     //logging.AddDebug();
                     //logging.AddEventLog();
                     logging.AddNLog("nlog.config");
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Limits.MaxConcurrentConnections = 1500;
                        serverOptions.Limits.MaxConcurrentUpgradedConnections = 1500;

                        //serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                        //serverOptions.Limits.MinRequestBodyDataRate =
                        //    new MinDataRate(bytesPerSecond: 100,
                        //        gracePeriod: TimeSpan.FromSeconds(10));
                        //serverOptions.Limits.MinResponseDataRate =
                        //    new MinDataRate(bytesPerSecond: 100,
                        //        gracePeriod: TimeSpan.FromSeconds(10));
                        
                        //serverOptions.Listen(IPAddress.Loopback, 5000);
                        //serverOptions.Listen(IPAddress.Loopback, 5001,
                        //    listenOptions =>
                        //    {
                        //        listenOptions.UseHttps("testCert.pfx",
                        //            "testPassword");
                        //    });
                        
                        serverOptions.Limits.KeepAliveTimeout =
                            TimeSpan.FromMinutes(2);
                        serverOptions.Limits.RequestHeadersTimeout =
                            TimeSpan.FromMinutes(1);
                    });

                    webBuilder.UseUrls("http://+:7006");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
