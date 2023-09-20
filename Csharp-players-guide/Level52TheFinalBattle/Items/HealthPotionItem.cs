namespace Level52TheFinalBattle.Items;

public class HealthPotionItem : ConsumableItem
{
    public int HealingPower { get; init; }

    public HealthPotionItem(int healingPower = 10)
        : base("HealingPotion")
    {
        HealingPower = healingPower;
    }

    public override string ToString()
    {
        return $"{Name} ({HealingPower})";
    }
}
