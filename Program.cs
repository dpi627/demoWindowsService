namespace demoWindowsService;

class Program
{
    static Task Main(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseWindowsService() // 設定為 Windows Service
            .ConfigureServices(services => services.AddHostedService<Worker>())
            .Build()
            .RunAsync();
    }
}
