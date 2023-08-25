using Level31TheFountainOfObjects.Interfaces;
using Level31TheFountainOfObjects.Enums;

namespace Level31TheFountainOfObjects.Providers;

public class ConsoleOutputProvider: IOutputInterface
{
    public void Output(string output, OutputType outputType, bool writeLine=true)
    {
        ConsoleColor consoleColor;
        switch (outputType)
        {
            case OutputType.Descriptive:
            case OutputType.Question:
                consoleColor = ConsoleColor.White;
                break;
            case OutputType.Fountain:
                consoleColor = ConsoleColor.Blue;
                break;
            case OutputType.Narrative:
            case OutputType.Maelstrom:
                consoleColor = ConsoleColor.Magenta;
                break;
            case OutputType.EntranceLight:
                consoleColor = ConsoleColor.Yellow;
                break;
            case OutputType.Warning:
            case OutputType.Pit:
                consoleColor = ConsoleColor.Red;
                break;
            case OutputType.Win:
            case OutputType.Success:
                consoleColor = ConsoleColor.Green;
                break;
            case OutputType.Empty:
            case OutputType.Help:
                consoleColor = ConsoleColor.Gray;
                break;
            case OutputType.Amarok:
                consoleColor = ConsoleColor.DarkRed;
                break;
            default:
                consoleColor = ConsoleColor.White;
                break;
        }

        Console.ForegroundColor = consoleColor;
        if (writeLine) Console.WriteLine(output);
        else Console.Write(output);
        Console.ResetColor();
    }
}