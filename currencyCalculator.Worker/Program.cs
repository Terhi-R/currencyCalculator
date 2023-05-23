using Coravel;
using currencyCalculator.Business.Services;
class Program
{
public static void Main(string[] args)
{
    IHost host = CreateHostBuilder(args).Build();
    host.Services.UseScheduler(scheduler => {
        scheduler
            .Schedule<InvokeDb>()
            .DailyAtHour(12);
    });
    host.Run();
}

public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            services.AddScheduler();
            services.AddTransient<InvokeDb>();
            services.AddScoped<IRatesHandler, RatesHandler>();
            services.AddScoped<ICurrencyConverterClient, CurrencyConverterClient>();
        });
}