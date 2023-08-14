// Challenge name: Repairing the Clocktower
// Objectives:
//      • Take a number as input from the console.
//      • Display the word “Tick” if the number is even. Display the word “Tock” if the number is odd.

namespace _09_decision_making.Challenges
{
    public class Challenge1
    {
        public static void Run()
        {
            Console.Write("Give me a number: ");
            int numberReceived = Convert.ToInt32(Console.ReadLine());

            if (numberReceived % 2 == 0)
            {
                Console.WriteLine("Tick");
            }
            else
            {
                Console.WriteLine("Tock");

            }
        }
    }
}