namespace demoWindowsService;

public class Worker(
    ILogger<Worker> logger,
    IConfiguration config
    ) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;

    // 服務啟動時執行一次
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("服務正在啟動...");
        return base.StartAsync(cancellationToken);
    }

    // 主要的背景執行邏輯
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int interval = config.GetValue<int>("Worker:Interval");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // 在這裡加入您的業務邏輯
                // 例如：檔案監控、資料處理等

                await Task.Delay(interval, stoppingToken); // 每5秒執行一次
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "執行時發生錯誤");
            }
        }
    }

    // 服務停止時執行一次
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("服務正在停止...");
        return base.StopAsync(cancellationToken);
    }
}
