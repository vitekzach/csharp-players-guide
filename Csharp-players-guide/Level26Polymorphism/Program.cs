using System.Data;
using Level26Polymorphism.Challenges;

// int maxItems = 5;
// double maxWeight = 5;
// double maxVolume = 5;
//
//
// Console.WriteLine("Creating pack with:");
// Console.WriteLine($" Max item count limit: {maxItems}");
// Console.WriteLine($" Max item weight limit: {maxWeight}");
// Console.WriteLine($" Max item volume limit: {maxVolume}");
// Pack pack = new Pack(maxItems, maxWeight, maxVolume);
//
// while (true)
// {   
//     Console.WriteLine(pack.ToString());
//     Console.WriteLine("Choose item to add from menu:");
//     Console.WriteLine(" (0) Arrow");
//     Console.WriteLine(" (1) Bow");
//     Console.WriteLine(" (2) Rope");
//     Console.WriteLine(" (3) Water");
//     Console.WriteLine(" (4) Food rations");
//     Console.WriteLine(" (5) Sword");
//     Console.Write("Your choice: ");
//     int choice = Convert.ToInt32(Console.ReadLine());
//     bool success = false;
//     success = choice switch
//     {
//         0 => pack.Add(new Arrow()),
//         1 => pack.Add(new Bow()),
//         2 => pack.Add(new Rope()),
//         3 => pack.Add(new Water()),
//         4 => pack.Add(new FoodRations()),
//         5 => pack.Add(new Sword()),
//     };
//     if (!success) Console.WriteLine("Adding was unsuccessful, item either can't fit or you didn't choose correct option.");
//     Console.WriteLine("-------------------------------------------------------------------");
// }

RobotCommand GetCommand(string command)
{
    RobotCommand resultingCommand = command switch
    {
        "on" => new OnCommand(),
        "off" => new OffCommand(),
        "east" => new EastCommand(),
        "west" => new WestCommand(),
        "north" => new NorthCommand(),
        "south" => new SouthCommand()
    };

    return resultingCommand;
}

Robot robot = new Robot {X = 0, Y = 0, IsPowered = false};
Console.WriteLine("Insert 3 commands: ");
robot.Commands[0] = GetCommand(Console.ReadLine() ?? "");
robot.Commands[1] = GetCommand(Console.ReadLine() ?? "");
robot.Commands[2] = GetCommand(Console.ReadLine() ?? "");
robot.Run();

