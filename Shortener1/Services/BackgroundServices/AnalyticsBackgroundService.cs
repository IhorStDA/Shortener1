using System.Collections.Concurrent;
using System.Threading.Channels;
using Asynq;
using Shortener1.Entities;

namespace Shortener1.Services.BackgroundServices;

public class AnalyticsBackgroundService : BackgroundService
{
    private readonly IMarketingDataService _marketingDataService;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly BlockingCollection<MarketingData> _analyticsQueue;
    private int _executionState;
    private readonly Channel<MarketingData> _analyticsChannel;

    public AnalyticsBackgroundService(IMarketingDataService marketingDataService, IHostApplicationLifetime appLifetime)
    {
        _marketingDataService = marketingDataService;
        _appLifetime = appLifetime;
        _analyticsQueue = new BlockingCollection<MarketingData>();
        _analyticsChannel = Channel.CreateUnbounded<MarketingData>();
    }


    private  void StartExecution()
    {
        if (!_analyticsQueue.IsAddingCompleted)
        {
            if (Interlocked.CompareExchange(ref _executionState, 1, 0) == 0)
            {
                Task.Run(() => ExecuteAsync(CancellationToken.None)).FireAndForget();
            }
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (await _analyticsChannel.Reader.WaitToReadAsync(stoppingToken))
            {
                while (_analyticsChannel.Reader.TryRead(out var mData))
                {
                    _ = _marketingDataService.Add(mData);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            _appLifetime.StopApplication();
        }
    }

    public void EnqueueAnalytics(MarketingData mData)
    {
        _analyticsChannel.Writer.TryWrite(mData);
        StartExecution();
    }
}