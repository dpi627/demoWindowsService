using Serilog;
using SGS.OAD.Helper;

namespace demoWindowsService;

class Program
{
    static Task Main(string[] args)
    {
        var builder = new HostApplicationBuilder(args);

        // 取得環境變數
        string environment = builder.Environment.EnvironmentName;
        string appName = builder.Environment.ApplicationName;
        string basePath = builder.Environment.ContentRootPath;

        // 設定組態檔
        builder.Configuration.SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

        // 設定 Serilog
        Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.WithProperty("Application", appName) //應用程式名稱不寫於設定檔
                .Enrich.WithProperty("Version", VersionHelper.CurrentVersion)
                .CreateLogger();

        Log.Information("{Application} 於 {EnvironmentName} 啟動", appName, environment);

        try
        {
            // 清除預設的日誌提供者
            builder.Logging.ClearProviders();
            // 使用 Serilog 取代內建的日誌機制
            builder.Logging.AddSerilog();

            // 設定為 Windows Service
            builder.Services.AddWindowsService();
            // 添加 Worker 服務
            builder.Services.AddHostedService<Worker>();

            // 構建和運行應用
            var host = builder.Build();
            return host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{Application} 啟動失敗", appName);
            return Task.FromException(ex);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
