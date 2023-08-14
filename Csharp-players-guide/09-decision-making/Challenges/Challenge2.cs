// Challenge name: Watchtower
// Objectives:
//      •Ask the user for an x value and a y value. These are coordinates of the enemy relative to the watchtower’s
//          location.
//      •Using the image on the right, if statements, and relational operators, display a message about what direction
//          the enemy is coming from. For example, “The enemy is to the northwest!” or “The enemy is here!”

namespace _09_decision_making.Challenges
{
    public class Challenge2
    {
        public static void Run()
        {
            Console.Write("Give me an x coordinate: ");
            int xCoordinate = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Give me a y coordinate: ");
            int yCoordinate = Convert.ToInt32(Console.ReadLine());

            string enemyDirection = "";

            if (yCoordinate > 0)
            {
                enemyDirection += "north";
            }
            else if (yCoordinate < 0)
            {
                enemyDirection += "south";
            }

            if (xCoordinate > 0)
            {
                enemyDirection += "east";
            }
            else if (xCoordinate < 0)
            {
                enemyDirection += "west";
            }

            if (enemyDirection == "")
            {
                Console.WriteLine("Enemy is right here!");
            }
            else
            {
                Console.WriteLine($"Enemy is to the {enemyDirection}!");
            }
        }
    }
}