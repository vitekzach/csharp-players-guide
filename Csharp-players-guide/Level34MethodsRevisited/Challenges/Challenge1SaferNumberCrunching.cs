namespace Level34MethodsRevisited.Challenges;

public class SafeNumberCruncher
{
    public static int CrunchIntNumber()
    {
        int parsedValue;
        do
        {
            Console.Write("Put integer in: ");
            string userInput = Console.ReadLine() ?? "";
            if (int.TryParse(userInput, out int value))
            {
                parsedValue = value; 
                break;
            }
            Console.WriteLine("You didn't put something I can parse. Try again...");
        } while (true);
        return parsedValue;
    }
    
    public static double CrunchDoubleNumber()
    {
        double parsedValue;
        do
        {
            Console.Write("Put a double in: ");
            string userInput = Console.ReadLine() ?? "";
            if (double.TryParse(userInput, out double value))
            {
                parsedValue = value; 
                break;
            }
            Console.WriteLine("You didn't put something I can parse. Try again...");
        } while (true);
        return parsedValue;
    }
    
    public static bool CrunchBool()
    {
        bool parsedValue;
        do
        {
            Console.Write("Put a bool in: ");
            string userInput = Console.ReadLine() ?? "";
            if (bool.TryParse(userInput, out bool value))
            {
                parsedValue = value; 
                break;
            }
            Console.WriteLine("You didn't put something I can parse. Try again...");
        } while (true);
        return parsedValue;
    }
}