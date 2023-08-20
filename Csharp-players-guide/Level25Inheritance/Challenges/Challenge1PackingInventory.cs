// •Create an InventoryItem class that represents any of the different item types. This class must
//          represent the item’s weight and volume, which it needs at creation time (constructor).
// •Create derived classes for each of the types of items above. Each class should pass the correct weight
//          and volume to the base class constructor but should be creatable themselves with a parameterless
//          constructor (for example, new Rope() or new Sword()).
// •Build a Pack class that can store an array of items. The total number of items, the maximum weight,
//          and the maximum volume are provided at creation time and cannot change afterward.
// •Make a public bool Add(InventoryItem item) method to Pack that allows you to add items
//          of any type to the pack’s contents. This method should fail (return false and not modify the pack’s
//          fields) if adding the item would cause it to exceed the pack’s item, weight, or volume limit.
// •Add properties to Pack that allow it to report the current item count, weight, and volume, and the
//          limits of each.
// •Create a program that creates a new pack and then allow the user to add (or attempt to add) items
//          chosen from a menu.
// 
// 



namespace Level25Inheritance.Challenges;

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
}

public class Bow : InventoryItem
{
    private static readonly double _weight = 1;
    private static readonly double _volume = 4;
    
    public Bow() : base(_weight, _volume)
    {
    }
}

public class Rope : InventoryItem
{
    private static readonly double _weight = 1;
    private static readonly double _volume = 1.5;
    
    public Rope() : base(_weight, _volume)
    {
    }
}

public class Water : InventoryItem
{
    private static readonly double _weight = 2;
    private static readonly double _volume = 3;
    
    public Water() : base(_weight, _volume)
    {
    }
}

public class FoodRations : InventoryItem
{
    private static readonly double _weight = 1;
    private static readonly double _volume = 0.5;
    
    public FoodRations() : base(_weight, _volume)
    {
    }
}

public class Sword : InventoryItem
{
    private static readonly double _weight = 5;
    private static readonly double _volume = 3;
    
    public Sword() : base(_weight, _volume)
    {
    }
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
}