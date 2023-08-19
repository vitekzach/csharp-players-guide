// Challenge name: The Properties of Arrows
// Objectives: 
//      •Modify your Arrow class to use properties instead of GetX and SetX methods.
//      •Ensure the whole program can still run, and Vin can keep creating arrows with it.


using System.Runtime.CompilerServices;
using Level21Static.Enums;

namespace Level21Static.Challenges;

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
    
    public static Arrow CreateEliteArrow() => new Arrow(ArrowHead.Steel, 95, ArrowFletching.Plastic);
    public static Arrow CreateBeginnerArrow() => new Arrow(ArrowHead.Wood, 75, ArrowFletching.GooseFeathers);
    public static Arrow CreateMarksmanArrow() => new Arrow(ArrowHead.Steel, 65, ArrowFletching.GooseFeathers);
}

public static class CreateArrowConsole
{
    public static ArrowHead ChooseArrowHead()
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
    
    public static double ChooseShaftLength()
    {
        double shaftLength;
        do
        {
            Console.Write("Choose shaft length (between 60 and 100 cm): ");
            shaftLength = Convert.ToDouble(Console.ReadLine());
        } while (shaftLength < 60 || shaftLength > 100);

        return shaftLength;
    }
    
    public static ArrowFletching ChooseFletching()
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

    public static Arrow CreateCustomArrow()
    {
        ArrowHead arrowHead = ChooseArrowHead();
        double arrowShaftLength = ChooseShaftLength();
        ArrowFletching arrowFletching = ChooseFletching();

        return new Arrow(arrowHead, arrowShaftLength, arrowFletching);
    }

    public static Arrow GetArrow()
    {
        Console.WriteLine("Choose from following options:");
        Console.WriteLine("(1) Beginner arrow");
        Console.WriteLine("(2) Marksman arrow");
        Console.WriteLine("(3) Elite arrow");
        Console.WriteLine("(4) Custom arrow");
        Console.Write("Your choice: ");
        int arrowChoice = Convert.ToInt32(Console.ReadLine());
        switch (arrowChoice)
        {
            case 1:
                return Arrow.CreateBeginnerArrow();
            case 2:
                return Arrow.CreateMarksmanArrow();
            case 3:
                return Arrow.CreateEliteArrow();
            default:
                return CreateCustomArrow();
        }
    }
}




public class Challenge1
{
   
    public static void Run()
    {
        Arrow arrow = CreateArrowConsole.GetArrow();
        Console.WriteLine($"arrow.arrowHead: {arrow.HeadType}");
        Console.WriteLine($"arrow.shaftLength: {arrow.ShaftLength}");
        Console.WriteLine($"arrow.arrowFletching: {arrow.FletchingType}");
        Console.WriteLine($"arrow.cost: {arrow.Cost}");
    }
}
