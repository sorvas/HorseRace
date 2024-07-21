using HorseRace.Core.Models;

namespace HorseRace.Core;

public interface IHorseRaceStarter
{
    IEnumerable<Horse> Run(List<Horse> horses);
}