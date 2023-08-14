// Challenge name: The Magic Cannon
// Objectives:
//         •Write a program that will loop through the values between 1 and 100 and display what kind of blast the crew
//              should expect. (The % operator may be of use.)
//         •Change the color of the output based on the type of blast. (For example, red for Fire, yellow for Electric,
//              and blue for Electric and Fire).

namespace _11_looping.Challenges
{
    public class Challenge2
    {
        public static void Run()
        {
            for (int crankNumber=1; crankNumber<=100; crankNumber++)
            {
                string blastType = "";
                
                if (crankNumber % 3 == 0 && crankNumber % 5 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    blastType = "Combined";
                } 
                else if (crankNumber % 3 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    blastType = "Fire";
                } 
                else if (crankNumber % 5 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    blastType = "Electric";
                }
                else
                {
                    Console.ResetColor();
                    blastType = "Normal";
                }
                
                Console.WriteLine($"{crankNumber,3}: {blastType}");
            }
            Console.ResetColor();
        }
    }
}