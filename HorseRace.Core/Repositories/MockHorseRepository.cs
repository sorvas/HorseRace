using HorseRace.Core.Models;
using HorseRace.Core.Services;

namespace HorseRace.Core.Repositories;

public class MockHorseRepository(IHorseRaceParticipantsBuilder participantsBuilder) : IHorseRepository
{
    public Task<List<Horse>> GetHorsesForRaceAsync(int raceId, int numberOfHorses)
    {
        return Task.FromResult(participantsBuilder.CreateParticipants(numberOfHorses));
    }
}