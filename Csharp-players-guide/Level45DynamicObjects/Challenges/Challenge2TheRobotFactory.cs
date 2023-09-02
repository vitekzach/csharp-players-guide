using System.Dynamic;

namespace Level45DynamicObjects.Challenges;

public class TheRobotFactory
{
    public static void StartRobotFactory()
    {
        int turn = 0;
        string response;

        while (true)
        {
            turn++;
            Console.WriteLine($"You are producing robot #{turn}.");
            dynamic robot = new ExpandoObject();
            Console.Write("Do you want to name this robot? ");
            response = Console.ReadLine() ?? "";
            if (response == "yes")
            {
                Console.Write("What is its name? ");
                response = Console.ReadLine() ?? "";
                robot.Name = response;
            }

            Console.Write("Does this robot have a specific size? ");
            response = Console.ReadLine() ?? "";
            if (response == "yes")
            {
                Console.Write("What is its height? ");
                response = Console.ReadLine() ?? "";
                robot.Height = response;

                Console.Write("What is its width? ");
                response = Console.ReadLine() ?? "";
                robot.Width = response;
            }

            Console.Write("Do you need to be a specific color? ");
            response = Console.ReadLine() ?? "";
            if (response == "yes")
            {
                Console.Write("What color? ");
                response = Console.ReadLine() ?? "";
                robot.Color = response;
            }

            foreach (KeyValuePair<string, object> property in (IDictionary<string, object>)robot)
            {
                Console.WriteLine($"{property.Key}: {property.Value}");
            }

        }
    }
}