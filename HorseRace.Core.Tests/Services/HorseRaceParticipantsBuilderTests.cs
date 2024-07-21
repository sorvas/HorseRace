using HorseRace.Core.Models;
using HorseRace.Core.Services;
using Microsoft.Extensions.Options;

namespace HorseRace.Core.Tests.Services;

public class HorseRaceParticipantsBuilderTests
{
    [Fact]
    public void CreateParticipants_ReturnsCorrectNumberOfHorses()
    {
        // Arrange
        IOptions<HorseRaceOptions> options = Options.Create(new HorseRaceOptions { MinHorses = 2, MaxHorses = 10, MinPower = 1, MaxPower = 15 });
        Random random = new(123);
        HorseRaceParticipantsBuilder builder = new(options, random);

        // Act
        List<Horse> horses = builder.CreateParticipants(5);

        // Assert
        Assert.Equal(5, horses.Count);
        Assert.Equal(5, horses.Select(h => h.Name).Distinct().Count());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(13)]
    public void CreateParticipants_ThrowsExceptionForInvalidCount(int count)
    {
        // Arrange
        IOptions<HorseRaceOptions> options = Options.Create(new HorseRaceOptions { MinHorses = 2, MaxHorses = 12 });
        Random random = new();
        HorseRaceParticipantsBuilder builder = new(options, random);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.CreateParticipants(count));
    }
}