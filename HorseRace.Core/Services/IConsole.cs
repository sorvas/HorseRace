namespace HorseRace.Core.Services;

public interface IConsole
{
    void WriteLine(string message);
    void Write(string message);
    void ReadKey();
}