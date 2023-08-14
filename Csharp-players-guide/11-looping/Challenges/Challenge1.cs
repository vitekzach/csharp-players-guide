// Challenge name: The Prototype
// Objectives:
//      •Build a program that will allow a user, the pilot, to enter a number.
//      •If the number is above 100 or less than 0, keep asking.
//      •Clear the screen once the program has collected a good number.
//      •Ask a second user, the hunter, to guess numbers.
//      •Indicate whether the user guessed too high, too low, or guessed right.
//      •Loop until they get it right, then end the program.

namespace _11_looping.Challenges
{
    public class Challenge1
    {
        public static void Run()
        {
            int chosenNumber;
            do
            {
                Console.Write("User 1, enter a number between 1 and 100: ");
                chosenNumber = Convert.ToInt32(Console.ReadLine());
            } while (chosenNumber <= 0 || chosenNumber > 100);
            
            Console.Clear();
            
            while (true)
            {
                Console.Write("User 2, guess the number: ");
                int guessedNumber = Convert.ToInt32(Console.ReadLine());
                if (guessedNumber > chosenNumber)
                {
                    Console.WriteLine($"{guessedNumber} is too high.");
                } 
                else if (guessedNumber < chosenNumber)
                {
                    Console.WriteLine($"{guessedNumber} is too low.");
                }
                else
                {
                    Console.WriteLine("You guessed the number!");
                    break;
                }
            }
        }
    }
}