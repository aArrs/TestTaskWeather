using ForecastBackgroundService;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ForecastService>();
    })
    .UseWindowsService()
    .Build();

await builder.RunAsync();

