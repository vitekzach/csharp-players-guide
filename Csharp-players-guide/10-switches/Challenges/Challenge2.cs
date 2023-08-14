// Challenge name: Discounted Inventory
// Objectives:
//          •Modify your program from before to also ask the user for their name.
//          •If their name equals your name, divide the cost in half.


namespace _10_stiches.Challenges
{
    public class Challenge2
    {
        public static void Run()
        {
            float finalPrice;
            string favoriteShopperName = "Vitek";
            
            Console.WriteLine("The following items are available:");
            Console.WriteLine("1 – Rope");
            Console.WriteLine("2 – Torches");
            Console.WriteLine("3 – Climbing Equipment");
            Console.WriteLine("4 – Clean Water");
            Console.WriteLine("5 – Machete");
            Console.WriteLine("6 – Canoe");
            Console.WriteLine("7 – Food Supplies");
            Console.Write("What number do you want to see the price of? ");
            int itemNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("What is your name? ");
            string shopperName = Console.ReadLine();

            int regularPrice = itemNumber switch
            {
                1 => 10,
                2 => 16,
                3 => 24,
                4 => 2,
                5 => 20,
                6 => 200,
                7 => 2,
            };

            finalPrice = shopperName == favoriteShopperName ? (float) regularPrice / 2 : regularPrice;
            
            Console.WriteLine($"Your item costs {finalPrice:#.##} gold piece(s).");
        }
    }
}