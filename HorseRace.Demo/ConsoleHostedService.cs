using HorseRace.Core.Models;
using HorseRace.Core.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HorseRace.Demo;

public class ConsoleHostedService(
    IHostApplicationLifetime appLifetime,
    IHorseRaceService horseRaceService,
    IOptions<HorseRaceOptions> options,
    Random random)
    : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    var raceId = random.Next(1000, 9999);
                    var numberOfHorses = random.Next(options.Value.MinHorses, options.Value.MaxHorses + 1);
                    var raceDistance = random.Next(1000, 2500); // Random distance between 1000m and 2500m

                    await horseRaceService.RunSimulationAsync(raceId, numberOfHorses, raceDistance);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    appLifetime.StopApplication();
                }
            }, cancellationToken);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}