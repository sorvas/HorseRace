using HorseRace.Core.Models;
using HorseRace.Core.Services;
using Microsoft.Extensions.Options;

namespace HorseRace.Core;

public class HorseRaceStarter(IOptions<HorseRaceOptions> options, Random random, int raceDistance = 1200)
    : IHorseRaceStarter
{
    public IEnumerable<Horse> Run(List<Horse> horses)
    {
        foreach (Horse horse in horses)
        {
            PowerCalculator.CalculateAndSetPower(horse, raceDistance);
        }

        return horses.OrderByDescending(horse => 
            horse.Power + random.NextDouble() * options.Value.RandomFactor); // Random factor to create some variation and give weaker horses a chance
    }
}