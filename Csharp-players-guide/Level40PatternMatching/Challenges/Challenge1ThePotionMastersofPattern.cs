using Level40PatternMatching.Enums;

namespace Level40PatternMatching.Challenges;

public class Potion
{
    public List<PotionIngredient> Ingredients { set; get; }
    public PotionType Type { get; set; }

    public Potion()
    {
        Ingredients = new List<PotionIngredient>() { PotionIngredient.Water };
        Type = PotionType.Potion;
        // Name = "potion";
    }

    public void Reset()
    {
        Ingredients = new List<PotionIngredient>() { PotionIngredient.Water };
        Type = PotionType.Potion;
    }

    public void ReportStatus()
    {
        Console.WriteLine($"Potion Type: {Type.ToString()}");
        Console.WriteLine($"Your ingredients: {String.Join(", ", Ingredients)}");
    }
}

public class PotionManager
{
    private Potion  ManagedPotion { get; set; }

    public PotionManager()
    {
        ManagedPotion = new Potion();
        ManagedPotion.ReportStatus();
        
        while (true)
        {
            AddIngredientToPotion();
            ManagedPotion.ReportStatus();
            if (AskToEnd()) break;
            Console.WriteLine("____________________");
            
        }
    }

    private bool AskToEnd()
    {
        Console.Write("Are you happy with your potion? (yes/no)");
        string response = Console.ReadLine() ?? "";
        if (response == "yes") return true;
        return false;
    }
    
    private void AddIngredientToPotion()
    {
        PotionIngredient ingredient = GetIngredient();
        ManagedPotion.Ingredients.Add(ingredient);
        
        switch (Potion: ManagedPotion, ingredient)
        {
            case (Potion {Type: PotionType.Potion}, PotionIngredient.Stardust):
                ManagedPotion.Type = PotionType.Elixir;
                break;
            case (Potion {Type: PotionType.Elixir}, PotionIngredient.SnakeVenom):
                ManagedPotion.Type = PotionType.Poison;
                break;
            case (Potion {Type: PotionType.Elixir}, PotionIngredient.DragonBreath):
                ManagedPotion.Type = PotionType.Flying;
                break;
            case (Potion {Type: PotionType.Elixir}, PotionIngredient.ShadowGlass):
                ManagedPotion.Type = PotionType.Invisibility;
                break;
            case (Potion {Type: PotionType.Elixir}, PotionIngredient.EyeshineGem):
                ManagedPotion.Type = PotionType.NightSight;
                break;
            case (Potion {Type: PotionType.NightSight}, PotionIngredient.ShadowGlass):
                ManagedPotion.Type = PotionType.Cloudy;
                break;
            case (Potion {Type: PotionType.Invisibility}, PotionIngredient.EyeshineGem):
                ManagedPotion.Type = PotionType.Cloudy;
                break;
            case (Potion {Type: PotionType.Cloudy}, PotionIngredient.Stardust):
                ManagedPotion.Type = PotionType.Wraith;
                break;
            default:
                ManagedPotion.Type = PotionType.Ruined;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You've ruined your potion! Start over with water.");
                Console.ResetColor();
                ManagedPotion.Reset();
                break;
        }
    }
    
    private PotionIngredient GetIngredient()
    {
        Console.WriteLine("You can pick from following ingredients: ");
        Console.WriteLine(" (1): Stardust");
        Console.WriteLine(" (2): SnakeVenom");
        Console.WriteLine(" (3): DragonBreath");
        Console.WriteLine(" (4): ShadowGlass");
        Console.WriteLine(" (5): EyeshineGem");
        Console.Write("Your choice: ");
        int ingredientChoice = Convert.ToInt32(Console.ReadLine());
        PotionIngredient ingredient = (ingredientChoice) switch
        {
            1 => PotionIngredient.Stardust,
            2 => PotionIngredient.SnakeVenom,
            3 => PotionIngredient.DragonBreath,
            4 => PotionIngredient.ShadowGlass,
            5 => PotionIngredient.EyeshineGem,
        };

        return ingredient;
    }
}