// Challenge name: The Dominion of Kings
// Objectives:
//      •Create a program that allows users to enter how many provinces, duchies, and estates they have.
//      •Add up the user’s total score, giving 1 point per estate, 3 per duchy, and 6 per province.
//      •Display the point total to the user.

namespace _07_math.Challenges;

public class Challenge3
{
    public static void Run()
    {
        uint pointsFromOwnership = 0;
        
        uint provincePoints = 6;
        uint duchiesPoints = 3;
        uint estatesPoints = 1;

        Console.Write($"Input amount of provinces (worth {provincePoints} points each): ");
        uint provincesOwned = Convert.ToUInt32(Console.ReadLine());
        pointsFromOwnership += provincePoints * provincesOwned;

        Console.Write($"Input amount of duchies (worth {duchiesPoints} points each): ");
        uint duchiesOwned = Convert.ToUInt32(Console.ReadLine());
        pointsFromOwnership += duchiesPoints * duchiesOwned;

        Console.Write($"Input amount of estates (worth {estatesPoints} point each): ");
        uint estatesOwned = Convert.ToUInt32(Console.ReadLine());
        pointsFromOwnership += estatesPoints * estatesOwned;

        Console.WriteLine($"Your ownership amounts to {pointsFromOwnership} points. How wonderful.");
    }
}