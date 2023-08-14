// Challenge name: The Replicator of D’To
// Objectives:
//      •Make a program that creates an array of length 5.
//      •Ask the user for five numbers and put them in the array.
//      •Make a second array of length 5.
//      •Use a loop to copy the values out of the original array and into the new one.
//      •Display the contents of both arrays one at a time to illustrate that the Replicator of D’To works again.

namespace _12_arrays.Challenges
{
    public class Challenge1
    {
        public static void Run()
        {
            int arraySize = 5;
            int[] originalArray = new int[arraySize];
            
            for (int i = 0; i < arraySize; i++)
            {
                Console.Write($"Please choose number {i+1,2} for original array: ");
                originalArray[i] = Convert.ToInt32(Console.ReadLine());
            }
            
            Console.WriteLine("Starting replicator...");

            int[] newArray = new int[arraySize];
            
            for (int i = 0; i < arraySize; i++)
            {
                newArray[i] = originalArray[i];
                Console.WriteLine($"Original value: {originalArray[i],3}, Replicated value: {newArray[i],3}");
            }

        }
    }
}