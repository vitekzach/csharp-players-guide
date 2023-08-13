//Challenge name: Consolas and Telim
// Goal: Ask who the bread is for.

namespace _03_hello_world.Challenges
{
    public class Challenge4
    {
        public static void Run()
        {
            Console.WriteLine("Bread is ready.");
            Console.WriteLine("Who is the bread for?");

            string name;
            name = Console.ReadLine();
            
            Console.WriteLine($"Noted: {name} got bread.");
        }
    }
}