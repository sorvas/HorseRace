using HorseRace.Core;
using HorseRace.Core.Models;
using HorseRace.Core.Repositories;
using HorseRace.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HorseRace.Demo;

class Program
{
    static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<HorseRaceOptions>(
                    hostContext.Configuration.GetSection("HorseRaceOptions"));

                services.AddSingleton<Random>();
                services.AddSingleton<IHorseRaceStarter, HorseRaceStarter>();
                services.AddSingleton<IHorseRaceParticipantsBuilder, HorseRaceParticipantsBuilder>();
                services.AddSingleton<IHorseRaceService, HorseRaceService>();
                services.AddSingleton<IHorseRepository, MockHorseRepository>();
                services.AddSingleton<IConsole, RealConsole>();

                services.AddHostedService<ConsoleHostedService>();
            });
}