using System.Diagnostics.CodeAnalysis;
using HorseRace.Core.Models;
using HorseRace.Core.Repositories;

namespace HorseRace.Core.Services;

[SuppressMessage("S106", "Required for console application")]
public class HorseRaceService(
    IHorseRaceStarter horseRaceStarter,
    IHorseRepository horseRepository,
    IConsole console) : IHorseRaceService
{
    public async Task RunSimulationAsync(int raceId, int numberOfHorses, int raceDistance)
    {
        console.WriteLine($"Welcome to the Horse HorseRaceStarter Simulator! HorseRaceStarter ID: {raceId}, Distance: {raceDistance}m");

        List<Horse> horses = await horseRepository.GetHorsesForRaceAsync(raceId, numberOfHorses);

        // Calculate and set power for each horse
        foreach (Horse horse in horses)
        {
            PowerCalculator.CalculateAndSetPower(horse, raceDistance);
        }

        DisplayParticipants(horses, raceDistance);

        console.WriteLine("\nPress any key to start the horseRaceStarter...");
        console.ReadKey();

        List<Horse> results = horseRaceStarter.Run(horses).ToList();

        DisplayResults(results);
    }

    private void DisplayParticipants(IEnumerable<Horse> horses, int raceDistance)
    {
        console.WriteLine($"\nParticipants for {raceDistance}m horseRaceStarter:");
        foreach (Horse horse in horses)
        {
            console.WriteLine(horse.ToString());
            if (horse is { Ability: not null, RaceHistory: not null })
            {
                console.WriteLine(
                    $"  Age: {horse.Ability.Age}, Weight: {horse.Ability.Weight}kg, Win%: {horse.RaceHistory.WinPercentage}%");
                console.WriteLine(
                    $"  Condition: {horse.Ability.Condition}, Speed: {horse.Ability.Speed}, Stamina: {horse.Ability.Stamina}");
                console.WriteLine($"  Power: {horse.Power}/15");
            }

            console.WriteLine("");
        }
    }

    private void DisplayResults(List<Horse> results)
    {
        console.WriteLine("\nRace Results:");
        for (var i = 0; i < results.Count; i++)
        {
            console.WriteLine(
                $"{i + 1}. {results[i].Name} (Jockey: {results[i].Registration?.Jockey}, Power: {results[i].Power}/15)");
        }
    }
}