// Challenge name: The Defense of Consolas
// Objectives:
//      •Ask the user for the target row and column.
//      •Compute the neighboring rows and columns of where to deploy the squad.
//      •Display the deployment instructions in a different color of your choosing.
//      •Change the window title to be “Defense of Consolas”.
//      •Play a sound with Console.Beep when the results have been computed and displayed.

namespace _08_console_2.Challenges
{
    public class Challenge1
    {
        public static void Run()
        {
            Console.Title = "Defense of Consolas";
            
            Console.Write("What's the target row? ");
            int targetRow = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("What's the target column? ");
            int targetColumn = Convert.ToInt32(Console.ReadLine());

            string neighborUp = $"({targetRow - 1,3}, {targetColumn,3})";
            string neighborRight = $"({targetRow,3}, {targetColumn + 1,3})";
            string neighborDown = $"({targetRow + 1,3}, {targetColumn,3})";
            string neighborLeft = $"({targetRow ,3}, {targetColumn - 1,3})";
            
            Console.WriteLine("Deploy to:");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(neighborUp);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(neighborRight);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(neighborDown);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(neighborLeft);
            Console.ResetColor();
            Console.WriteLine("Calculation done.");
            // Console.Beep(440, 1000);
            // Console.Beep(800, 1000);
            // Console.Beep(1000, 1000);
            
        }
    }
}