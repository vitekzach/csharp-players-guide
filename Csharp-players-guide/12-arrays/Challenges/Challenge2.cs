// Challenge name: The Laws of Freach
// Objectives:
//      •Start with the code for computing an array’s minimum and average values in the section Some Examples with
//          Arrays, starting on page 94.
//      •Modify the code to use foreach loops instead of for loops.

namespace _12_arrays.Challenges
{
    public class Challenge2
    {
        public static void Run()
        {
            int[] array = new int[] { 4, 51, -7, 13, -99, 15, -8, 45, 90 };

            int arrayMinimum = int.MaxValue;
            float arrayAverage = 0;
            int arrayTotal = 0;
            
            foreach (int arrayNumber in array)
            {
                if (arrayNumber < arrayMinimum)
                    arrayMinimum = arrayNumber;

                arrayTotal += arrayNumber;
            }

            arrayAverage = (float)arrayTotal / array.Length;
            
            Console.WriteLine($"Array's minimum is {arrayMinimum}.");
            Console.WriteLine($"Array's average is {arrayAverage:#,##}.");
        }
    }
}