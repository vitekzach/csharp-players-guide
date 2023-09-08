using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Records;
namespace Level52TheFinalBattle.Helpers;


public static class ConsoleHelpers
{

    public static void WriteWithColoredConsole(MessageType messageType, string text)
    {
        Console.ForegroundColor = GetConsoleColorFromMessageType(messageType);
        Console.Write(text);
        Console.ResetColor();
    }


    public static void WriteLineWithColoredConsole(MessageType messageType, string text)
    {
        Console.ForegroundColor = GetConsoleColorFromMessageType(messageType);
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static int GetValidConsoleIntegerInput(string prompt)
    {
        return GetValidInput<int>(prompt, CheckIntegerValid);
    }

    public static string GetValidConsoleStringInput(string prompt)
    {
        return GetValidInput<string>(prompt, CheckStringValid);
    }

    private static T GetValidInput<T>(string prompt, Func<string, ConsoleTypeCheckOutput<T>> getParsedInput)
    {
        ConsoleTypeCheckOutput<T> convertResult;

        do
        {
            WriteWithColoredConsole(MessageType.Choice,$"{prompt} ");
            string? userInput = Console.ReadLine();
            convertResult = getParsedInput(userInput);
            if (!convertResult.Success) WriteLineWithColoredConsole(MessageType.Error, convertResult.ErrorMessage);

        } while(!convertResult.Success);

        return convertResult.ParsedValue;
    }

    private static ConsoleTypeCheckOutput<int> CheckIntegerValid(string? userInput)
    {
        bool success = int.TryParse(userInput, out int parsedValue);
        return new ConsoleTypeCheckOutput<int>(success, parsedValue, "You need to input an integer.");
    }

    private static ConsoleTypeCheckOutput<string> CheckStringValid(string? userInput)
    {
        bool success = userInput != null;
        return new ConsoleTypeCheckOutput<string>(success, userInput, "You need to input a valid string.");
    }

    private static ConsoleColor GetConsoleColorFromMessageType(MessageType messageType)
    {
        return messageType switch
        {
            MessageType.Normal => ConsoleColor.White,
            MessageType.Battle => ConsoleColor.Green,
            MessageType.Choice => ConsoleColor.Yellow,
            MessageType.Error => ConsoleColor.Red,
            MessageType.Team => ConsoleColor.DarkBlue,
            MessageType.Time => ConsoleColor.Blue
        };
    }
    // private static ConsoleTypeCheckOutput<T> CheckValidInput<T>(string? userInput, out T parsedValue)
    // {
    //     Type typeOfT = typeof(T);
    //
    //     switch (typeOfT)
    //     {
    //         case Type stringType when stringType == typeof(int):
    //             bool success = int.TryParse(userInput, out int parsedIntValue);
    //             return new ConsoleTypeCheckOutput<int>(success, parsedIntValue, "You must input an integer!");
    //             break;
    //     }
    // }
}