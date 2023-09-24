using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Interfaces;

namespace Level52TheFinalBattle.Items;

public class InventoryItem : IUsable //, IComparable
{
    public string Name { get; init; }

    public InventoryItem(string name)
    {
        Name = name;
    }

    public void Use(Character target, Party targetParty)
    {
        target.UseItem(this, targetParty);
    }

    // public static bool operator <(InventoryItem a, InventoryItem b)
    // {
    //     if (a is HealthPotionItem potionA && b is HealthPotionItem potionB)
    //     {
    //         return potionA.HealingPower < potionB.HealingPower;
    //     }
    //     if (a is HealthPotionItem)
    //         return true;
    //     if (a is GearItem gearA && b is GearItem gearB)
    //         return string.Compare(gearA.Name, gearB.Name, StringComparison.CurrentCulture) <= 0;
    //     return false;
    // }

    // public static bool operator >(InventoryItem a, InventoryItem b)
    // {
    //     return b < a;
    // }

    // public int CompareTo(object? obj)
    // {
    //     if (obj != null && obj is InventoryItem invItem)
    //     {
    //         if (this < invItem)
    //             return -1;
    //         else
    //             return 1;
    //     }
    //     return 999;
    // }

    public override string ToString()
    {
        return $"{Name}";
    }
}

public class InventoryItemComparer : IComparer<InventoryItem>
{
    public int Compare(InventoryItem? x, InventoryItem? y)
    {
        if (x is HealthPotionItem potionA && y is HealthPotionItem potionB)
        {
            return potionA.HealingPower - potionB.HealingPower;
        }

        if (x is HealthPotionItem)
            return -1;

        if (x is GearItem gearA && y is GearItem gearB)
            return gearA.GearAttack.MaxDamage - gearB.GearAttack.MaxDamage;
        return 0;
    }
}
