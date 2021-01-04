using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;

namespace TestCache
{
    public class BackgroundService : IHostedService, IDisposable
    {
        private Timer _timer;
        private int executionCount = 0;
        private readonly ILogger<BackgroundService> _logger;
        private IIDTPCache _idtpCache;
        private string lastStoredValue = String.Empty;

        public BackgroundService(ILogger<BackgroundService> logger, IIDTPCache idtpCache) {
            _logger = logger;
            _idtpCache = idtpCache;
        }

        public Task StartAsync(CancellationToken stoppingToken) {
            _logger.LogInformation("Background Service Started.");

            //$TODO: the following duraton should be changed after testing
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                    TimeSpan.FromSeconds(6000));

            return Task.CompletedTask;
        }

        private void DoWork(object state) {
            var count = Interlocked.Increment(ref executionCount);
            // _logger.LogInformation("BackgroundService Main Loop Count: {Count}", count);

            if (count == 1) { //do the following iteration only once since it'll 
                              //Block and wait for the next item in the Cached Collection

                foreach (var item in _idtpCache.GetCollection()) {
                    DBUtility.SimpleInsert(item);
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken) {
            //  _logger.LogInformation("Background Service is Stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose() {
            _timer?.Dispose();
        }
    }
} //namespace