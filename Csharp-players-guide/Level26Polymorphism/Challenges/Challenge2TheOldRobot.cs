// •Copy the code above into a new project.
// •Create a RobotCommand class with a public and abstract void Run(Robot robot) method. (The
//      code above should compile after this step.)
// •Make OnCommand and OffCommand classes that inherit from RobotCommand and turn the robot
//      on or off by overriding the Run method.
// •Make a NorthCommand, SouthCommand, WestCommand, and EastCommand that move the robot 1
//      unit in the +Y direction, 1 unit in the -Y direction, 1 unit in the -X direction, and 1 unit in the +X
//      direction, respectively. Also, ensure that these commands only work if the robot’s IsPowered
//      property is true.
// •Make your main method able to collect three commands from the console window. Generate new
//      RobotCommand objects based on the text entered. After filling the robot’s command set with these
//      new RobotCommand objects, use the robot’s Run method to execute them.

namespace Level26Polymorphism.Challenges;

public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public RobotCommand?[] Commands { get; } = new RobotCommand?[3];
    public void Run()
    {
        foreach (RobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public abstract class RobotCommand
{
    public abstract void Run(Robot robot);
}

public class OnCommand: RobotCommand
{
    public override void Run(Robot robot)
    {
        if (!robot.IsPowered) robot.IsPowered = true;
    }
}

public class OffCommand: RobotCommand
{
    public override void Run(Robot robot)
    {
        if (robot.IsPowered) robot.IsPowered = false;
    }
}

public class NorthCommand: RobotCommand
{
    public override void Run(Robot robot)
    {
        if (robot.IsPowered) robot.Y++;
    }
}

public class SouthCommand: RobotCommand
{
    public override void Run(Robot robot)
    {
        if (robot.IsPowered) robot.Y--;
    }
}

public class EastCommand: RobotCommand
{
    public override void Run(Robot robot)
    {
        if (robot.IsPowered) robot.X++;
    }
}

public class WestCommand: RobotCommand
{
    public override void Run(Robot robot)
    {
        if (robot.IsPowered) robot.X--;
    }
}
