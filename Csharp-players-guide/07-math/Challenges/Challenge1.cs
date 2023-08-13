// Challenge name: The Triangle Farmer
// Objective:
    // •Write a program that lets you input the triangle’s base size and height.
    // •Compute the area of a triangle by turning the above equation into code.
    // •Write the result of the computation.
namespace _07_math.Challenges
{
    public class Challenge1
    {
        public static void Run()
        {
            Console.Write("Input base: ");
            float triangleBase = Convert.ToSingle(Console.ReadLine());
            
            Console.Write("Input height: ");
            float triangleHeight = Convert.ToSingle(Console.ReadLine());

            float triangleArea = (triangleBase * triangleHeight) / 2;
            Console.WriteLine($"Your triangle area is: {triangleArea} units^2.");
        }
    }
}