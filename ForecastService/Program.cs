using ForecastBackgroundService;
using ForecastServices.FunctionalClassess;

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
    })
    .UseWindowsService()
    .Build();


await builder.RunAsync();
