using Level27Interfaces.Challenges;

IRobotCommand GetCommand(string command)
{
    IRobotCommand resultingCommand = command switch
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

