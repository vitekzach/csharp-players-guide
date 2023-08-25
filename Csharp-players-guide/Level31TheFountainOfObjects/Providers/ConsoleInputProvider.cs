using Level31TheFountainOfObjects.Interfaces;

namespace Level31TheFountainOfObjects.Providers;

public class ConsoleInputProvider: IInputInterface
{
    public string Input()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        string userInput = Console.ReadLine() ?? "";
        Console.ResetColor();
        return userInput;
    }
}