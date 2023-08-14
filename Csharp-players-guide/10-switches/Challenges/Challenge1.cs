// Challenge name: Buying Inventory
// Prices:
//      Rope: 10 gold,
//      Torches: 16 gold,
//      Climbing Equipment: 24 gold,
//      Clean Water: 2 gold,
//      Machete: 20 gold,
//      Canoe: 200 gold,
//      Food Supplies: 2 gold.
// Objectives:
//      •Build a program that will show the menu illustrated above.
//      •Ask the user to enter a number from the menu.
//      •Using the information above, use a switch (either type) to show the item’s cost.


namespace _10_stiches.Challenges
{
    public class Challenge1
    {
        public static void Run()
        {
            int priceRope = 10;
            int priceTorches = 16;
            int priceClimbingEquipment = 24;
            int priceCleanWater = 2;
            int priceMachete = 20;
            int priceCanoe = 200;
            int priceFoodSupplies = 2;
            
            Console.WriteLine("The following items are available:");
            Console.WriteLine("1 – Rope");
            Console.WriteLine("2 – Torches");
            Console.WriteLine("3 – Climbing Equipment");
            Console.WriteLine("4 – Clean Water");
            Console.WriteLine("5 – Machete");
            Console.WriteLine("6 – Canoe");
            Console.WriteLine("7 – Food Supplies");
            Console.Write("What number do you want to see the price of?");
            
            int itemNumber = Convert.ToInt32(Console.ReadLine());

            switch (itemNumber)
            {
                case 1:
                    Console.WriteLine($"Rope costs {priceRope} gold.");
                    break;
                case 2:
                    Console.WriteLine($"Torches cost {priceTorches} gold.");
                    break;
                case 3:
                    Console.WriteLine($"Climbing Equipment costs {priceClimbingEquipment} gold.");
                    break;
                case 4:
                    Console.WriteLine($"Clean Water costs {priceCleanWater} gold.");
                    break;
                case 5:
                    Console.WriteLine($"Machete costs {priceMachete} gold.");
                    break;
                case 6:
                    Console.WriteLine($"Canoe costs {priceCanoe} gold.");
                    break;
                case 7:
                    Console.WriteLine($"Food Supplies cost {priceFoodSupplies} gold.");
                    break;
                default:
                    Console.WriteLine("You picked an invalid number.");
                    break;
            }

            string response = itemNumber switch
            {
                1 => $"Rope costs {priceRope} gold.",
                2 => $"Torches cost {priceTorches} gold.",
                3 => $"Climbing Equipment costs {priceClimbingEquipment} gold.",
                4 => $"Clean Water costs {priceCleanWater} gold.",
                5 => $"Machete costs {priceMachete} gold.",
                6 => $"Canoe costs {priceCanoe} gold.",
                7 => $"Food Supplies cost {priceFoodSupplies} gold.",
                _ => "You picked an invalid number.",

            };
            
            Console.WriteLine(response);
        }
    }
}