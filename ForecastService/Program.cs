using ForecastBackgroundService;
using ForecastServices.Interfaces;
using ForecastServices;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ForecastService>();
        services.AddTransient<WeatherHandler>();
        services.AddSingleton<IDataProvider, DataProvider>();
        services.AddTransient<IEntityProvider, EntityProvider>();
        services.AddTransient<IMessageBuilder, BuildMessage>();
        services.AddTransient<IMailSender, SendMail>();
        services.AddTransient<IAddToDb, AddToDb>();
        services.AddTransient<IGetDbData, GetDbData>();
        services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<WeatherHandler>>());
        services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<BuildMessage>>());
        services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<AddToDb>>());
        services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<DataProvider>>());
        services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<EntityProvider>>());
        services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<SendMail>>());
    })
    .UseWindowsService()
    .Build();

await builder.RunAsync();
