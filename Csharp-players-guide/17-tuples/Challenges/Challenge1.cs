// Challenge name: Simula’s Soup
// Objectives: 
//      •Define enumerations for the three variations on food: type (soup, stew, gumbo), main ingredient
//          (mushrooms, chicken, carrots, potatoes), and seasoning (spicy, salty, sweet).
//      •Make a tuple variable to represent a soup composed of the three above enumeration types.
//      •Let the user pick a type, main ingredient, and seasoning from the allowed choices and fill the tuple with
//          the results. Hint: You could give the user a menu to pick from or simply compare the user’s text input
//          against specific strings to determine which enumeration value represents their choice.
//      •When done, display the contents of the soup tuple variable in a format like “Sweet Chicken Gumbo.”
//      Hint: You don’t need to convert the enumeration value back to a string. Simply displaying an enumeration
//          value with Write or WriteLine will display the name of the enumeration value.)


using _17_tuples.Enums;

namespace _17_tuples.Challenges;

public class Challenge1
{
    public static SoupType GetSoupType()
    {
        Console.WriteLine("Pick the soup type from following menu:");
        Console.WriteLine($"0: {(SoupType) 0}");
        Console.WriteLine($"1: {(SoupType) 1}");
        Console.WriteLine($"2: {(SoupType) 2}");
        Console.Write("Put in number corresponding to wanted type: ");
        int soupTypeChoice = Convert.ToInt32(Console.ReadLine());
        return (SoupType)soupTypeChoice;
    }
    
    public static SoupMainIngredient GetSoupMainIngredient()
    {
        Console.WriteLine("Pick the soup main ingredient from following menu:");
        Console.WriteLine($"0: {(SoupMainIngredient) 0}");
        Console.WriteLine($"1: {(SoupMainIngredient) 1}");
        Console.WriteLine($"2: {(SoupMainIngredient) 2}");
        Console.WriteLine($"3: {(SoupMainIngredient) 3}");
        Console.Write("Put in number corresponding to wanted ingredient: ");
        int soupMainIngredient = Convert.ToInt32(Console.ReadLine());
        return (SoupMainIngredient)soupMainIngredient;
    }
    
    public static SoupSeasoning GetSoupSeasoning()
    {
        Console.WriteLine("Pick the soup seasoning from following menu:");
        Console.WriteLine($"0: {(SoupSeasoning) 0}");
        Console.WriteLine($"1: {(SoupSeasoning) 1}");
        Console.WriteLine($"2: {(SoupSeasoning) 2}");
        Console.Write("Put in number corresponding to wanted seasoning: ");
        int soupSeasoningChoice = Convert.ToInt32(Console.ReadLine());
        return (SoupSeasoning)soupSeasoningChoice;
    }
    
    public static void Run()
    {
        (SoupType soupType, SoupMainIngredient soupMainIngredient, SoupSeasoning soupSeasoning) soupIngredients;

        soupIngredients.soupType = GetSoupType();
        soupIngredients.soupMainIngredient = GetSoupMainIngredient();
        soupIngredients.soupSeasoning = GetSoupSeasoning();
        
        Console.WriteLine($"{soupIngredients.soupSeasoning} {soupIngredients.soupMainIngredient} {soupIngredients.soupType}");

    }
}
