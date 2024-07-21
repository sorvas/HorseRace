using HorseRace.Core.Models;
using Microsoft.Extensions.Options;

namespace HorseRace.Core.Services;

public class HorseRaceParticipantsBuilder(IOptions<HorseRaceOptions> options, Random random)
    : IHorseRaceParticipantsBuilder
{
    private readonly HorseRaceOptions _options = options.Value;

    private const int WinPercentageMin = 5;
    private const int WinPercentageMax = 31;
    
    private const int PlacePercentageMin = 10;
    private const int PlacePercentageMax = 51;
    
    private const int AbilityMin = 30;
    private const int AbilityMax = 100;
    
    private const int WeightMin = 450;
    private const int WeightMax = 550;
    
    private const int AgeMin = 2;
    private const int AgeMax = 8;
    
    private const int HorseSex = 2;
    
    public List<Horse> CreateParticipants(int numberOfHorses)
    {
        if (numberOfHorses < _options.MinHorses || numberOfHorses > _options.MaxHorses)
        {
            throw new ArgumentException(
                $"Number of horses must be between {_options.MinHorses} and {_options.MaxHorses}",
                nameof(numberOfHorses));
        }

        List<Horse> horses = [];
        List<string> names = GetUniqueHorseNames(numberOfHorses);

        for (var i = 0; i < numberOfHorses; i++)
        {
            horses.Add(CreateHorse(names[i], i + 1));
        }

        return horses;
    }

    private Horse CreateHorse(string name, int number)
    {
        return new Horse
        {
            Name = name,
            Ability = CreateAbility(),
            RaceHistory = CreateRaceHistory(),
            Registration = CreateRegistration(number)
        };
    }

    private Ability CreateAbility()
    {
        return new Ability
        {
            Condition = random.Next(AbilityMin, AbilityMax),
            Speed = random.Next(AbilityMin, AbilityMax),
            Stamina = random.Next(AbilityMin, AbilityMax),
            StartingAbility = random.Next(AbilityMin, AbilityMax),
            Competitiveness = random.Next(AbilityMin, AbilityMax),
            Weight = random.Next(WeightMin, WeightMax),
            Age = random.Next(AgeMin, AgeMax),
            Sex = random.Next(HorseSex) == 0 ? "Colt" : "Filly",
            Odds = GenerateOdds()
        };
    }

    private RaceHistory CreateRaceHistory()
    {
        return new RaceHistory
        {
            WinPercentage = random.Next(WinPercentageMin, WinPercentageMax),
            PlacePercentage = random.Next(PlacePercentageMin, PlacePercentageMax)
        };
    }

    private Registration CreateRegistration(int number)
    {
        return new Registration
        {
            Jockey = GetRandomJockeyName(),
            Trainer = GetRandomTrainerName(),
            Owner = GetRandomOwnerName(),
            Number = number
        };
    }

    private List<string> GetUniqueHorseNames(int count)
    {
        List<string> allNames =
        [
            "Thunderbolt", "Silver Arrow", "Midnight Star", "Golden Hoof", "Whispering Wind",
            "Storm Runner", "Velvet Hooves", "Lightning Streak", "Mountain Spirit", "River Dash",
            "Desert Wind", "Forest Whisper", "Ocean Breeze", "Starlight Gallop", "Mystic Mane",
            "Opal Odyssey", "Jade Jester", "Amber Anthem", "Crystal Canter", "Nebula Dash",
            "Avalanche Sprint", "Phoenix Flare", "Zephyr Zoom", "Mirage Mover", "Glacier Glide",
            "Tornado Trotter", "Eclipse Enigma", "Blizzard Bolt", "Horizon Hustler"
        ];

        return allNames.OrderBy(_ => random.Next()).Take(count).ToList();
    }

    private string GetRandomJockeyName()
    {
        List<string> jockeys = ["John Smith", "Emma Johnson", "Michael Brown", "Sophia Davis", "William Wilson"];
        return jockeys[random.Next(jockeys.Count)];
    }

    private string GetRandomTrainerName()
    {
        List<string> trainers = ["Robert Taylor", "Olivia Anderson", "James Thomas", "Ava White", "Daniel Harris"];
        return trainers[random.Next(trainers.Count)];
    }

    private string GetRandomOwnerName()
    {
        List<string> owners = ["Charles Martin", "Elizabeth Clark", "George Rodriguez", "Isabella Lewis", "Henry Lee"];
        return owners[random.Next(owners.Count)];
    }

    private string GenerateOdds()
    {
        var numerator = random.Next(1, 21);
        const int denominator = 1;
        return $"{numerator}-{denominator}";
    }
}