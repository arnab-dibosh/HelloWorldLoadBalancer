using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Helper;

namespace TestCache
{
    public class BackgroundTransService5 : IHostedService, IDisposable
    {
        private Timer _timer;
        private int executionCount = 0;
        private readonly ILogger<BackgroundTransService5> _logger;
        private IIDTPTransCache _idtpTransCache;
        private string lastStoredValue = String.Empty;

        public BackgroundTransService5(ILogger<BackgroundTransService5> logger, IIDTPTransCache idtpTransCache)
        {
            //_logger = logger;
            _idtpTransCache = idtpTransCache;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            //_logger.LogInformation("Background Service Started.");

            //$TODO: the following duraton should be changed after testing
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                    TimeSpan.FromSeconds(6000));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);
            // _logger.LogInformation("BackgroundService Main Loop Count: {Count}", count);

            SqlConnection connection = new SqlConnection(DBUtility.ConnectionString);
            connection.Open();
            if (count == 1) { //do the following iteration only once since it'll 
                              //Block and wait for the next item in the Cached Collection
                foreach (var item in _idtpTransCache.GetTransCollection()) {
                    DBUtility.TransferFundFinalSp(item, connection);
                }
            }
            connection.Close();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            //  _logger.LogInformation("Background Service is Stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
