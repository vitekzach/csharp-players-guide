// Challenge name: Countdown
// Objectives:
//      •Write code that counts down from 10 to 1 using a recursive method.

namespace _13_methods.Challenges
{
    public class Challenge2
    {
        public static void CountDown(int countFrom)
        {
            Console.WriteLine(countFrom);
            if (countFrom == 1) return;
            
            CountDown(countFrom - 1);
        }
        public static void Run()
        {
            CountDown(10);
        }
    }
}