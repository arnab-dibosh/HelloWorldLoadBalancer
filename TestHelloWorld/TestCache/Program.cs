using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCache
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
          .ConfigureLogging((hostingContext, logging) => {
              logging.ClearProviders();
          })
          .ConfigureWebHostDefaults(webBuilder => {
              webBuilder.ConfigureKestrel(serverOptions => {
                  serverOptions.Limits.MaxConcurrentConnections = 1500;
                  serverOptions.Limits.MaxConcurrentUpgradedConnections = 1500;
                  serverOptions.Limits.KeepAliveTimeout =
                      TimeSpan.FromMinutes(2);
                  serverOptions.Limits.RequestHeadersTimeout =
                      TimeSpan.FromMinutes(1);
              });
              webBuilder.UseUrls("http://+:7008");
              webBuilder.UseStartup<Startup>();
          });
    }
}
