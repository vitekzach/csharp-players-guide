using Level25Inheritance.Challenges;

int maxItems = 5;
double maxWeight = 5;
double maxVolume = 5;


Console.WriteLine("Creating pack with:");
Console.WriteLine($" Max item count limit: {maxItems}");
Console.WriteLine($" Max item weight limit: {maxWeight}");
Console.WriteLine($" Max item volume limit: {maxVolume}");
Pack pack = new Pack(maxItems, maxWeight, maxVolume);

while (true)
{   
    pack.ReportContents();
    Console.WriteLine("Choose item to add from menu:");
    Console.WriteLine(" (0) Arrow");
    Console.WriteLine(" (1) Bow");
    Console.WriteLine(" (2) Rope");
    Console.WriteLine(" (3) Water");
    Console.WriteLine(" (4) Food rations");
    Console.WriteLine(" (4) Sword");
    Console.Write("Your choice: ");
    int choice = Convert.ToInt32(Console.ReadLine());
    bool success = false;
    success = choice switch
    {
        0 => pack.Add(new Arrow()),
        1 => pack.Add(new Bow()),
        2 => pack.Add(new Rope()),
        3 => pack.Add(new Water()),
        4 => pack.Add(new FoodRations()),
        5 => pack.Add(new Sword()),
    };
    if (!success) Console.WriteLine("Adding was unsuccessful, item either can't fit or you didn't choose correct option.");
    Console.WriteLine("-------------------------------------------------------------------");
}