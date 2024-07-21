using HorseRace.Core.Models;
using HorseRace.Core.Services;

namespace HorseRace.Core.Tests.Services;

public class PowerCalculatorTests
{
    [Fact]
    public void CalculateAndSetPower_ShouldSetPowerWithinValidRange()
    {
        // Arrange
        Horse horse = new Horse
        {
            Ability = new Ability
            {
                Condition = 80,
                Speed = 85,
                Stamina = 75,
                StartingAbility = 70,
                Competitiveness = 90,
                Age = 4,
                Weight = 500
            },
            RaceHistory = new RaceHistory
            {
                WinPercentage = 30
            }
        };
        const int raceDistance = 1200;

        // Act
        PowerCalculator.CalculateAndSetPower(horse, raceDistance);

        // Assert
        Assert.InRange(horse.Power, 1, 15);
    }

    [Fact]
    public void CalculateAndSetPower_ShouldThrowExceptionForIncompleteHorse()
    {
        // Arrange
        Horse incompleteHorse = new Horse(); // No Ability or RaceHistory

        // Act & Assert
        Assert.Throws<ArgumentException>(() => PowerCalculator.CalculateAndSetPower(incompleteHorse, 1000));
    }

    [Theory]
    [InlineData(3, 500, 20, 1000, 1, 15)] // Young horse
    [InlineData(5, 450, 50, 1800, 1, 15)] // Prime age, lighter weight, longer distance
    [InlineData(7, 550, 10, 800, 1, 15)]  // Older horse, heavier weight, shorter distance
    public void CalculateAndSetPower_ShouldHandleVariousScenarios(int age, int weight, double winPercentage, int distance, int minExpectedPower, int maxExpectedPower)
    {
        // Arrange
        Horse horse = new Horse
        {
            Ability = new Ability
            {
                Condition = 80,
                Speed = 80,
                Stamina = 80,
                StartingAbility = 80,
                Competitiveness = 80,
                Age = age,
                Weight = weight
            },
            RaceHistory = new RaceHistory
            {
                WinPercentage = winPercentage
            }
        };

        // Act
        PowerCalculator.CalculateAndSetPower(horse, distance);

        // Assert
        Assert.InRange(horse.Power, minExpectedPower, maxExpectedPower);
    }
}