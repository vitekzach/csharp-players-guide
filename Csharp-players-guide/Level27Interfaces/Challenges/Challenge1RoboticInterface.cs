// •Change your abstract RobotCommand class into an IRobotCommand interface.
// •Remove the unnecessary public and abstract keywords from the Run method.
// •Change the Robot class to use IRobotCommand instead of RobotCommand.
// •Make all of your commands implement this new interface instead of extending the RobotCommand
//      class that no longer exists. You will also want to remove the override keywords in these classes.
// •Ensure your program still compiles and runs.
// •Answer this question: Do you feel this is an improvement over using an abstract base class? Why
//      or why not?

namespace Level27Interfaces.Challenges;

public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public IRobotCommand?[] Commands { get; } = new IRobotCommand?[3];
    public void Run()
    {
        foreach (IRobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public interface IRobotCommand
{
    void Run(Robot robot);
}

public class OnCommand: IRobotCommand
{
    public void Run(Robot robot)
    {
        if (!robot.IsPowered) robot.IsPowered = true;
    }
}

public class OffCommand: IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) robot.IsPowered = false;
    }
}

public class NorthCommand: IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) robot.Y++;
    }
}

public class SouthCommand: IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) robot.Y--;
    }
}

public class EastCommand: IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) robot.X++;
    }
}

public class WestCommand: IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) robot.X--;
    }
}
