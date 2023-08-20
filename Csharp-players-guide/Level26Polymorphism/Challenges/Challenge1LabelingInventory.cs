// •Override the existing ToString method (from the object base class) on all of your inventory item
//      subclasses to give them a name. For example, new Rope().ToString() should return "Rope".
// •Override ToString on the Pack class to display the contents of the pack. If a pack contains water,
//      rope, and two arrows, then calling ToString on that Pack object could look like "Pack
//      containing Water Rope Arrow Arrow".
// •Before the user chooses the next item to add, display the pack’s current contents via its new
//      ToString method.

namespace Level26Polymorphism.Challenges;

public class InventoryItem
{
    public double Weight { get; init; }
    public double Volume { get; init; }

    public InventoryItem(double weight, double volume)
    {
        Weight = weight;
        Volume = volume;
    }
}

public class Arrow : InventoryItem
{
    private static readonly double _weight = 0.1;
    private static readonly double _volume = 0.05;
    
    public Arrow() : base(_weight, _volume)
    {
    }
    
    public override string ToString() => "Arrow";
}

public class Bow : InventoryItem
{
    private static readonly double _weight = 1;
    private static readonly double _volume = 4;
    
    public Bow() : base(_weight, _volume)
    {
    }
    public override string ToString() => "Bow";
}

public class Rope : InventoryItem
{
    private static readonly double _weight = 1;
    private static readonly double _volume = 1.5;
    
    public Rope() : base(_weight, _volume)
    {
    }

    public override string ToString() => "Rope";
}

public class Water : InventoryItem
{
    private static readonly double _weight = 2;
    private static readonly double _volume = 3;
    
    public Water() : base(_weight, _volume)
    {
    }
    
    public override string ToString() => "Water";
}

public class FoodRations : InventoryItem
{
    private static readonly double _weight = 1;
    private static readonly double _volume = 0.5;
    
    public FoodRations() : base(_weight, _volume)
    {
    }
    
    public override string ToString() => "FoodRations";
}

public class Sword : InventoryItem
{
    private static readonly double _weight = 5;
    private static readonly double _volume = 3;
    
    public Sword() : base(_weight, _volume)
    {
    }
    
    public override string ToString() => "Sword";
}

public class Pack
{
    public InventoryItem[] StoredItems { get; private set; }
    public int MaxItemsCount { get; init; }
    public double MaxWeight { get; init; }
    public double MaxVolume { get; init; }
    public int StoredItemsCount { get; private set; } = 0;
    public double StoredItemsWeight { get; private set; } = 0;
    public double StoredItemsVolume { get; private set; } = 0;

    public Pack(int maxItemsCount, double maxWeight, double maxVolume)
    {
        StoredItems = new InventoryItem[maxItemsCount];
        MaxItemsCount = maxItemsCount;
        MaxWeight = maxWeight;
        MaxVolume = maxVolume;
    }

    public bool Add(InventoryItem item)
    {
        int newStoredItemsCount = StoredItemsCount + 1;
        double newStoredItemsWeight = StoredItemsWeight + item.Weight;
        double newStoredItemsVolume = StoredItemsVolume + item.Volume;

        if (newStoredItemsCount <= MaxItemsCount && newStoredItemsWeight <= MaxWeight &&
            newStoredItemsVolume <= MaxVolume)
        {
            StoredItemsCount = newStoredItemsCount;
            StoredItemsWeight = newStoredItemsWeight;
            StoredItemsVolume = newStoredItemsVolume;
            StoredItems[StoredItemsCount-1] = item;
            return true;
        }

        return false;
    }

    public void ReportContents()
    {
        Console.WriteLine("Backpack contents:");
        Console.WriteLine($" Items: {StoredItems}");
        Console.WriteLine($" Items: {StoredItems[0]}");
        Console.WriteLine($" Count: {StoredItemsCount}");
        Console.WriteLine($" Weight: {StoredItemsWeight}");
        Console.WriteLine($" Volume: {StoredItemsVolume}");
        
    }

    public override string ToString()
    {
        string contents = "Pack containing ";
        foreach (InventoryItem item in StoredItems) contents += $"{item?.ToString()} ";
        return contents;
    }
}