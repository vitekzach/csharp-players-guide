// •Modify your Hunting the Manticore game to be a single-player game by having the computer pick a
//      random range between 0 and 100.
// •Answer this question: How might you use inheritance, polymorphism, or interfaces to allow the
//      game to be either a single player (the computer randomly chooses the starting location and
//      direction) or two players (the second human determines the starting location and direction)?

namespace Level32SomeUsefulTypes.Challenges;

using System;

public class TheRobotPilot
    {
        public static int GetManticoreDistance()
        {
            Random random = new Random();
            return random.Next(0, 101);
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
