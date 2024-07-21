using HorseRace.Core.Models;

namespace HorseRace.Core.Repositories;

public interface IHorseRepository
{
    Task<List<Horse>> GetHorsesForRaceAsync(int raceId, int numberOfHorses);
}