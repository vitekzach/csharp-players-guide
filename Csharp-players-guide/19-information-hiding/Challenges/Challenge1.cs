// Challenge name: Simula’s Soup
// Objectives: 
//      •Modify your Arrow class to have private instead of public fields.
//      •Add in getter methods for each of the fields that you have.


using _19_information_hiding.Enums.ArrowPartsEnums;

namespace _19_information_hiding.Challenges;

public class Arrow
{
    private ArrowHead arrowHead;
    private double shaftLength;
    private ArrowFletching arrowFletching;

    public Arrow(ArrowHead arrowHead, double shaftLength, ArrowFletching arrowFletching)
    {
        this.arrowHead = arrowHead;
        this.shaftLength = shaftLength;
        this.arrowFletching = arrowFletching;
    }

    public Arrow()
    {
        this.arrowHead = SetArrowHead();
        this.shaftLength = SetShaftLength();
        this.arrowFletching = SetFletching();
    }

    private ArrowHead SetArrowHead()
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
    
    private double SetShaftLength()
    {
        double shaftLength;
        do
        {
            Console.Write("Choose shaft length (between 60 and 100 cm): ");
            shaftLength = Convert.ToDouble(Console.ReadLine());
        } while (shaftLength < 60 || shaftLength > 100);

        return shaftLength;
    }
    
    private ArrowFletching SetFletching()
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

    public ArrowHead GetArrowHead() => this.arrowHead;
    public double GetShaftLength() => this.shaftLength;
    public ArrowFletching GetArrowFletching() => this.arrowFletching;
}


public class Challenge1
{
   
    public static void Run()
    {
        Arrow arrow = new Arrow();
        Console.WriteLine($"arrow.arrowHead: {arrow.GetArrowHead()}");
        Console.WriteLine($"arrow.shaftLength: {arrow.GetShaftLength()}");
        Console.WriteLine($"arrow.arrowFletching: {arrow.GetArrowFletching()}");
        Console.WriteLine($"arrow.cost: {arrow.GetCost()}");
    }
}
