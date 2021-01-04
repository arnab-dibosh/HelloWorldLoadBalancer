using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace TestCache
{ 
public class TimedHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer _timer;

    private IIDTPCache _idtpCache;

    public TimedHostedService(ILogger<TimedHostedService> logger, IIDTPCache idtpCache)
    {
        _logger = logger;
        _idtpCache = idtpCache;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(60));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        var count = Interlocked.Increment(ref executionCount);

        _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);

            //read and output value from Cache
            //var cachedValue = _idtpCache.GetValue();
            //_logger.LogInformation("Cached Value: {Value}", cachedValue.ToString());

            foreach (var item in _idtpCache.GetCollection())
            {
                _logger.LogInformation("Cached Value: {Value}", item.ToString());
            }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
} //namespace