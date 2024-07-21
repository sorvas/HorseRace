namespace HorseRace.Core.Services;

public interface IHorseRaceService
{
    Task RunSimulationAsync(int raceId, int numberOfHorses, int raceDistance);
}