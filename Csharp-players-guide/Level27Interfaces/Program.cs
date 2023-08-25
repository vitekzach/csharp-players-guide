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
Console.WriteLine("Keep inserting commands: ");
while (true)
{
    string command = Console.ReadLine() ?? "";
    if (command == "stop") break;
    robot.Commands.Add(GetCommand(command));
}
robot.Run();

