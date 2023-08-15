// Challenge name: 
// Objectives:
//      •Establish the game’s starting state: the Manticore begins with 10 health points and the city with 15. The game
//          starts at round 1.
//      •Ask the first player to choose the Manticore’s distance from the city (0 to 100). Clear the screen afterward.
//      •Run the game in a loop until either the Manticore’s or city’s health reaches 0.
//      •Before the second player’s turn, display the round number, the city’s health, and the Manticore’s health.
//      •Compute how much damage the cannon will deal this round: 10 points if the round number is a multiple of both
//          3 and 5, 3 if it is a multiple of 3 or 5 (but not both), and 1 otherwise. Display this to the player.
//      •Get a target range from the second player, and resolve its effect. Tell the user if they overshot (too far),
//          fell short, or hit the Manticore. If it was a hit, reduce the Manticore’s health by the expected amount.
//      •If the Manticore is still alive, reduce the city’s health by 1.
//      •Advance to the next round.
//      •When the Manticore or the city’s health reaches 0, end the game and display the outcome.
//      •Use different colors for different types of messages.
//      •Note: This is the largest program you have made so far. Expect it to take some time!
//      •Note: Use methods to focus on solving one problem at a time.
//      •Note: This version requires two players, but in the future, we will modify it to allow the computer

using System.Reflection.Metadata;

namespace _14_memory_management.Challenges
{
    public class Challenge1
    {
        public static int GetManticoreDistance()
        {
            int manticoreDistance;
            do
            {
                Console.Write("Player 1, how far away from the city do you want to station the Manticore? ");
                manticoreDistance = Convert.ToInt32(Console.ReadLine());
            } while (manticoreDistance < 0 || manticoreDistance > 100);
            
            Console.Clear();
            return manticoreDistance;
        }
        
        public static int[] InitGame()
        {
            return new int[3] {GetManticoreDistance(), 10, 15};
        }

        public static void PrintRoundStats(int roundNumber, int manticoreHealth, int cityHealth, int expectedDamage, int manticoreMaxHealth, int cityMaxHealth)
        {
            Console.WriteLine($"STATUS: Round: {roundNumber} City: {cityHealth}/{cityMaxHealth} Manticore: {manticoreHealth}/{manticoreMaxHealth}");
            Console.WriteLine($"The cannon is expected to deal {expectedDamage} damage this round.");
        }

        public static int GetExpectedDamage(int roundNumber)
        {
            if (roundNumber % 3 == 0 && roundNumber % 5 == 0) return 5;
            if (roundNumber % 3 == 0 || roundNumber % 5 == 0) return 3;
            return 1;
        }

        public static int GetShootDistance()
        {
            Console.Write("Enter desired cannon range: ");
            int enteredDistance = Convert.ToInt32(Console.ReadLine());
            return enteredDistance;
        }

        public static string GetShotResult(int manticoreDistance, int shootDistance)
        {
            if (manticoreDistance > shootDistance) return "too short";
            if (manticoreDistance < shootDistance) return "too far";
            return "hit";
        }

        public static void PrintShotResult(string shotResult)
        {
            string result = shotResult switch
            {
                "too short" => "FELL SHORT of the target.",
                "too far" => "OVERSHOT the target.",
                "hit" => "was a DIRECT HIT!",
            };

            Console.WriteLine($"That round {result}");
        }
        
        public static int[] UpdateHealths(int manticoreHealth, int cityHealth, int damageDealt, string shotResult)
        {
            if (shotResult == "hit") manticoreHealth -= damageDealt;
            if (manticoreHealth > 0) cityHealth--;
            return new int[] {manticoreHealth, cityHealth};
        }

        public static void InitRound()
        {
            Console.WriteLine("-----------------------------------------------------------");
        }
        
        public static void EndRound()
        {
            // Console.WriteLine("-----------------------------------------------------------");
        }

        public static int[] RunRound(int manticoreDistance, int roundNumber, int manticoreHealth, int cityHealth, int manticoreMaxHealth, int cityMaxHealth)
        {
            InitRound();
            int expectedDamage = GetExpectedDamage(roundNumber);
            PrintRoundStats(roundNumber, manticoreHealth, cityHealth, expectedDamage, manticoreMaxHealth, cityMaxHealth);
            int shootDistance = GetShootDistance();
            string shotResult = GetShotResult(manticoreDistance, shootDistance);
            PrintShotResult(shotResult);
            int[] healthsAfterRound = UpdateHealths(manticoreHealth, cityHealth, expectedDamage, shotResult);
            manticoreHealth = healthsAfterRound[0];
            cityHealth = healthsAfterRound[1];
            
            EndRound();
            return new int[] {manticoreHealth, cityHealth};
        }

        public static void PrintEnding(int manticoreHealth, int cityHealth)
        {
            if (manticoreHealth <= 0) Console.WriteLine("The Manticore has been destroyed! The city of Consolas has been saved!");
            if (cityHealth <= 0) Console.WriteLine("The city has been destroyed! The manticore is reigning terror on the region!");
        }
        
        public static void Run()
        {
            int[] initValues = InitGame();

            int roundNumber = 1;
            int manticoreDistance = initValues[0];
            int manticoreHealth = initValues[1];
            int manticoreMaxHealth = initValues[1];
            int cityHealth = initValues[2];
            int cityMaxHealth = initValues[2];

            while (manticoreHealth > 0 && cityHealth > 0)
            {
                int[] roundResult = RunRound(manticoreDistance, roundNumber, manticoreHealth, cityHealth,
                    manticoreMaxHealth, cityMaxHealth);
                manticoreHealth = roundResult[0];
                cityHealth = roundResult[1];
                roundNumber++;
            }
            PrintEnding(manticoreHealth, cityHealth);
        }
    }
}