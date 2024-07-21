namespace HorseRace.Core.Models;

public class Horse
{
    public string Name { get; init; } = string.Empty;
    public int Power { get; set; }
    
    public Ability? Ability { get; init; }
    public RaceHistory? RaceHistory { get; init; }
    public Registration? Registration { get; init; }
    public RaceHistory? History { get; set; }
    
    public override string ToString()
    {
        return $"{Registration?.Number}. {Name} (Jockey: {Registration?.Jockey}, Odds: {Ability?.Odds})";
    }
}

public class Registration
{
    public string? Jockey { get; init; }
    public string? Trainer { get; set; }
    public string? Owner { get; set; }
    public int Number { get; init; }
}

public class Ability
{
    // Ability scores (0-100)
    public int Condition { get; init; }
    public int Speed { get; init;  }
    public int Stamina { get; init;  }
    public int StartingAbility { get; init;  }
    public int Competitiveness { get; init; }
    
    public int Weight { get; init; }
    public int Age { get; init; }
    public string? Sex { get; set; }
    
    public string? Odds { get; init; }
}

public class RaceHistory
{
    public double WinPercentage { get; init; }
    public double PlacePercentage { get; set; }
}