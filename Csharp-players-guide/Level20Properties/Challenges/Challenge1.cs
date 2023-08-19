// Challenge name: The Properties of Arrows
// Objectives: 
//      •Modify your Arrow class to use properties instead of GetX and SetX methods.
//      •Ensure the whole program can still run, and Vin can keep creating arrows with it.


using Level20Properties.Enums;

namespace Level20Properties.Challenges;

public class Arrow
{
    public ArrowHead HeadType {get;}
    public double ShaftLength {get;}
    public ArrowFletching FletchingType {get;}

    public double Cost
    {
        get
        {
            int headCost = HeadType switch
            {
                ArrowHead.Obsidian => 5,
                ArrowHead.Steel => 10,
                ArrowHead.Wood => 3,
            };

            double shaftCost = ShaftLength * 0.05;

            int fletchingCost = FletchingType switch
            {
                ArrowFletching.Plastic => 10,
                ArrowFletching.GooseFeathers => 3,
                ArrowFletching.TurkeyFeathers => 5,
            };

            return headCost + shaftCost + fletchingCost;
        }
    }

    public Arrow(ArrowHead headType, double shaftLength, ArrowFletching fletchingType)
    {
        HeadType = headType;
        ShaftLength = shaftLength;
        FletchingType = fletchingType;
    }

    public Arrow()
    {
        HeadType = SetArrowHead();
        ShaftLength = SetShaftLength();
        FletchingType = SetFletching();
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
}


public class Challenge1
{
   
    public static void Run()
    {
        Arrow arrow = new Arrow();
        Console.WriteLine($"arrow.arrowHead: {arrow.HeadType}");
        Console.WriteLine($"arrow.shaftLength: {arrow.ShaftLength}");
        Console.WriteLine($"arrow.arrowFletching: {arrow.FletchingType}");
        Console.WriteLine($"arrow.cost: {arrow.Cost}");
    }
}
