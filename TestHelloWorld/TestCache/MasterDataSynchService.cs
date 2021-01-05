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
    public class MasterDataSynchService : IHostedService, IDisposable
    {
        private Timer _timer;
        private int executionCount = 0;
        private readonly ILogger<MasterDataSynchService> _logger;
        private IMasterDataCache _masterDataCache;
        private string lastStoredValue = String.Empty;

        public MasterDataSynchService(ILogger<MasterDataSynchService> logger, IMasterDataCache masterDataCache) {
            _masterDataCache = masterDataCache;
        }

        public Task StartAsync(CancellationToken stoppingToken) {

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(6000));

            return Task.CompletedTask;
        }

        private void DoWork(object state) {
            var count = Interlocked.Increment(ref executionCount);
            
            
            if (count == 1) {

                IDTPUtility.LoadMasterData(_masterDataCache,true);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken) {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose() {
            _timer?.Dispose();
        }
    }
}