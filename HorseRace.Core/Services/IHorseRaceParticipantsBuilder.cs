using HorseRace.Core.Models;

namespace HorseRace.Core.Services;

public interface IHorseRaceParticipantsBuilder
{
    List<Horse> CreateParticipants(int numberOfHorses);
}