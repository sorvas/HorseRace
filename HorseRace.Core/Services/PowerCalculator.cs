using HorseRace.Core.Models;

namespace HorseRace.Core.Services;

public static class PowerCalculator
{
    private const int MaxPower = 15;
    private const int MinPower = 1;
    private const int MiddleRangePowerCurve = 5;

    public static void CalculateAndSetPower(Horse horse, int raceDistance)
    {
        if (horse.Ability == null || horse.RaceHistory == null)
        {
            throw new ArgumentException("Horse must have Ability and RaceHistory set.");
        }

        // Normalize and exaggerate ability scores
        var normalizedCondition = Math.Pow(horse.Ability.Condition / 100.0, 2);
        var normalizedSpeed = Math.Pow(horse.Ability.Speed / 100.0, 2);
        var normalizedStamina = Math.Pow(horse.Ability.Stamina / 100.0, 2);
        var normalizedStartingAbility = Math.Pow(horse.Ability.StartingAbility / 100.0, 2);
        var normalizedCompetitiveness = Math.Pow(horse.Ability.Competitiveness / 100.0, 2);

        // Calculate base power with weighted abilities
        var basePower = (normalizedCondition * 0.2 + 
                         normalizedSpeed * 0.3 + 
                         normalizedStamina * 0.2 + 
                         normalizedStartingAbility * 0.15 + 
                         normalizedCompetitiveness * 0.15) * 10;

        // Adjust for race distance
        var distanceFactor = raceDistance > 1600 ? normalizedStamina : normalizedSpeed;
        var distanceAdjustment = distanceFactor * 2 - 1; // Range: -1 to 1

        // Consider past performance
        var performanceBonus = Math.Pow(horse.RaceHistory.WinPercentage / 100.0, 1.5) * 2;

        // Age factor (assuming prime age is 4-5 years)
        var ageFactor = horse.Ability.Age switch
        {
            < 3 => 0.8,
            3 => 0.9,
            4 => 1.1,
            5 => 1.1,
            6 => 1.0,
            > 6 => 1.0 - (horse.Ability.Age - 6) * 0.05
        };

        // Weight penalty
        var weightPenalty = 1 - Math.Pow((horse.Ability.Weight - 450) / 500.0, 2);

        // Calculate power score
        var powerScore = (basePower + distanceAdjustment + performanceBonus) * ageFactor * weightPenalty;

        // Sigmoid variant to calculate power
        horse.Power = (int)Math.Round(MinPower + (MaxPower - MinPower) / (1 + Math.Exp(-powerScore + MiddleRangePowerCurve)));
    }
}