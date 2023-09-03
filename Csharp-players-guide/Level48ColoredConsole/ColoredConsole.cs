namespace Level48ColoredConsole;


public static class ColoredConsole
{
    public static string Prompt(string question)
    {
        Console.Write(question);
        Console.ForegroundColor = ConsoleColor.Cyan;
        string answer = Console.ReadLine() ?? "";
        Console.ResetColor();
        return answer;
    }

    public static void WriteLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Write(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();

    }
}