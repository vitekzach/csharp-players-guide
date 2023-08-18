// Challenge name: Simula’s Soup
// Objectives: 
//      •Define a new Arrow class with fields for arrowhead type, fletching type, and length. (Hint: arrowhead types
//          and fletching types might be good enumerations.)
//      •Allow a user to pick the arrowhead, fletching type, and length and then create a new Arrow instance.
//      •Add a GetCost method that returns its cost as a float based on the numbers above, and use this to display the
//          arrow’s cost.


using _18_classes.Enums.ArrowPartsEnums;

namespace _18_classes.Challenges;

public class Arrow
{
    public ArrowHead arrowHead;
    public double shaftLength;
    public ArrowFletching arrowFletching;

    public Arrow(ArrowHead arrowHead, double shaftLength, ArrowFletching arrowFletching)
    {
        this.arrowHead = arrowHead;
        this.shaftLength = shaftLength;
        this.arrowFletching = arrowFletching;
    }

    public Arrow()
    {
        this.arrowHead = GetArrowHead();
        this.shaftLength = GetShaftLength();
        this.arrowFletching = GetFletching();
    }

    private ArrowHead GetArrowHead()
    {
        Console.Write("Choose arrow head type (steel, wood, or obsidian): ");
        string arrowHeadChoice = Console.ReadLine();
        ArrowHead arrowHead = arrowHeadChoice switch
        {
            "steel" => ArrowHead.Steel,
            "wood" => ArrowHead.Wood,
            "obsidian" => ArrowHead.Wood
        };

        return arrowHead;
    }
    
    private double GetShaftLength()
    {
        double shaftLength;
        do
        {
            Console.Write("Choose shaft length (between 60 and 100 cm): ");
            shaftLength = Convert.ToDouble(Console.ReadLine());
        } while (shaftLength < 60 || shaftLength > 100);

        return shaftLength;
    }
    
    private ArrowFletching GetFletching()
    {
        Console.Write("Choose arrow fletching type (plastic, turkey feathers, or goose feathers): ");
        string arrowFletchingChoice = Console.ReadLine();
        ArrowFletching arrowFletching = arrowFletchingChoice switch
        {
            "plastic" => ArrowFletching.Plastic,
            "turkey feathers" => ArrowFletching.TurkeyFeathers,
            "goose feathers" => ArrowFletching.GooseFeathers
        };

        return arrowFletching;
    }

    public double GetCost()
    {
        int headCost = arrowHead switch
        {
            ArrowHead.Obsidian => 5,
            ArrowHead.Steel => 10,
            ArrowHead.Wood => 3,
        };

        double shaftCost = shaftLength * 0.05;

        int fletchingCost = arrowFletching switch
        {
            ArrowFletching.Plastic => 10,
            ArrowFletching.GooseFeathers => 3,
            ArrowFletching.TurkeyFeathers => 5,
        };

        return headCost + shaftCost + fletchingCost;
    }
}


public class Challenge1
{
   
    public static void Run()
    {
        Arrow arrow = new Arrow();
        Console.WriteLine($"arrow.arrowHead: {arrow.arrowHead}");
        Console.WriteLine($"arrow.shaftLength: {arrow.shaftLength}");
        Console.WriteLine($"arrow.arrowFletching: {arrow.arrowFletching}");
        Console.WriteLine($"arrow.cost: {arrow.GetCost()}");
    }
}
