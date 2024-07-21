using System.Diagnostics.CodeAnalysis;

namespace HorseRace.Core.Services;

[SuppressMessage("S106", "Required for console application")]
public class RealConsole : IConsole
{
    public void WriteLine(string message) => Console.WriteLine(message);
    public void Write(string message) => Console.Write(message);
    public void ReadKey() => Console.ReadKey();
}