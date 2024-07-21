using HorseRace.Core.Models;
using HorseRace.Core.Repositories;
using HorseRace.Core.Services;
using Moq;

namespace HorseRace.Core.Tests.Services;

public class HorseRaceServiceTests
{
    private readonly Mock<IHorseRaceStarter> _mockHorseRaceStarter;
    private readonly Mock<IHorseRepository> _mockHorseRepository;
    private readonly Mock<IConsole> _mockConsole;
    private readonly HorseRaceService _service;

    public HorseRaceServiceTests()
    {
        _mockHorseRaceStarter = new Mock<IHorseRaceStarter>();
        _mockHorseRepository = new Mock<IHorseRepository>();
        _mockConsole = new Mock<IConsole>();
        _service = new HorseRaceService(
            _mockHorseRaceStarter.Object,
            _mockHorseRepository.Object,
            _mockConsole.Object);
    }

    [Fact]
    public async Task RunSimulationAsync_ShouldGetHorsesFromRepository()
    {
        // Arrange
        const int raceId = 1;
        const int numberOfHorses = 5;
        const int raceDistance = 1000;
        List<Horse> horses = CreateMockHorses(numberOfHorses);
        _mockHorseRepository.Setup(repo => repo.GetHorsesForRaceAsync(raceId, numberOfHorses))
            .ReturnsAsync(horses);
        _mockHorseRaceStarter.Setup(starter => starter.Run(It.IsAny<List<Horse>>()))
            .Returns(horses);

        // Act
        await _service.RunSimulationAsync(raceId, numberOfHorses, raceDistance);

        // Assert
        _mockHorseRepository.Verify(repo => repo.GetHorsesForRaceAsync(raceId, numberOfHorses), Times.Once);
    }

    [Fact]
    public async Task RunSimulationAsync_ShouldWaitForUserInput()
    {
        // Arrange
        const int raceId = 1;
        const int numberOfHorses = 5;
        const int raceDistance = 1000;
        List<Horse> horses = CreateMockHorses(numberOfHorses);
        _mockHorseRepository.Setup(repo => repo.GetHorsesForRaceAsync(raceId, numberOfHorses))
            .ReturnsAsync(horses);
        _mockHorseRaceStarter.Setup(starter => starter.Run(It.IsAny<List<Horse>>()))
            .Returns(horses);

        // Act
        await _service.RunSimulationAsync(raceId, numberOfHorses, raceDistance);

        // Assert
        _mockConsole.Verify(c => c.ReadKey(), Times.Once);
    }

    private static List<Horse> CreateMockHorses(int count)
    {
        List<Horse> horses = [];
        for (var i = 0; i < count; i++)
        {
            horses.Add(new Horse
            {
                Name = $"Horse {i + 1}",
                Power = 1,
                Ability = new Ability { Age = 3, Weight = 500, Condition = 80, Speed = 80, Stamina = 80 },
                RaceHistory = new RaceHistory { WinPercentage = 20 },
                Registration = new Registration { Jockey = $"Jockey {i + 1}" }
            });
        }

        return horses;
    }
}