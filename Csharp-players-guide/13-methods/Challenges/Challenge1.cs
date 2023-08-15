// Challenge name: Taking a Number
// Objectives:
//      •Make a method with the signature int AskForNumber(string text). Display the text parameter in the console
//              window, get a response from the user, convert it to an int, and return it. This might look like this:
//              int result = AskForNumber("What is the airspeed velocity of an unladen swallow?");.
//      •Make a method with the signature int AskForNumberInRange(string text, int min, int max). Only return if the
//              entered number is between the min and max values. Otherwise, ask again.
//      •Place these methods in at least one of your previous programs to improve it.

namespace _13_methods.Challenges
{
    public class Challenge1
    {
        /// <summary>
        /// Return asked for number.
        /// </summary>
        /// <param name="text">Query to put before asking for a number</param>
        /// <returns></returns>
        public static int AskForNumber(string text)
        {
            Console.Write(text);
            int number = Convert.ToInt32(Console.ReadLine());
            return number;
        }

        public static int AskForNumberInRange(string text, int min, int max)
        {
            int number;
            do
            {
                Console.Write(text);
                number = Convert.ToInt32(Console.ReadLine());
            } while (number < min || number > max);
            
            return number;
        }
        
        public static void Run()
        {
            int result = AskForNumber("What is the airspeed velocity of an unladen swallow?");
            Console.WriteLine($"It is {result}.");

            int result2 = AskForNumberInRange("What is the airspeed velocity of an unladen swallow?", 0, 30);
            Console.WriteLine($"It is {result2}.");
            
        }
    }
}