using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.Items;

public class HealthPotionItem : InventoryItem
{
    public int HealingPower { get; init; }

    internal HealthPotionItem(HealingItemEnum healingItemType)
        : base("HealingPotion")
    {
        switch (healingItemType)
        {
            case HealingItemEnum.SmallPotion:
                HealingPower = 5;
                Name = "Small Healing Potion";
                break;
            case HealingItemEnum.BigPotion:
                HealingPower = 10;
                Name = "Big Healing Potion";
                break;
            case HealingItemEnum.SimulasSoup:
                HealingPower = 9999999;
                Name = "Simula's soup";
                break;
            default:
                throw new NotImplementedException("Unkown healing item type encountered.");
        }
    }

    public override string ToString()
    {
        string valueBeforeHP = (HealingPower < 9999999) ? HealingPower.ToString() : "to full";
        return $"{Name} (heals {valueBeforeHP} HP)";
    }
}
