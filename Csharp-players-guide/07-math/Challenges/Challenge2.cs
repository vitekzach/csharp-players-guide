// Challenge name: The Four Sisters and the Duckbear
// Objectives: 
//      •Create a program that lets the user enter the number of chocolate eggs gathered that day.
//      •Using / and %, compute how many eggs each sister should get and how many are left over for the duckbear.
//      •Answer this question: What are three total egg counts where the duckbear gets more than each sister does?
//             You can use the program you created to help you find the answer.
                    // 1, 2, 3

namespace _07_math.Challenges
{
    public class Challenge2
    {
        public static void Run()
        {
            uint sisterCount = 4;
            Console.Write("Amount of eggs: ");
            uint eggsLaid = Convert.ToUInt32(Console.ReadLine());

            uint eachSisterGets = eggsLaid / sisterCount;
            uint duckbearGets = eggsLaid % sisterCount;
            
            Console.WriteLine($"Each sister gets {eachSisterGets} eggs.");
            Console.WriteLine($"Duckbear gets {duckbearGets} eggs.");
        }
    }
}