using System.Data.Common;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.Items;

public class GearItem : InventoryItem
{
    public Attack GearAttack { get; init; }
    public int RoundsToActivate { get; set; }

    public GearItem(string name, Attack attack, int roundsToActivate = 1)
        : base(name)
    {
        GearAttack = attack;
        RoundsToActivate = roundsToActivate;
    }

    public override string ToString()
    {
        return $"{Name} (equippable,  max {GearAttack.MaxDamage} dmg)";
    }
}
