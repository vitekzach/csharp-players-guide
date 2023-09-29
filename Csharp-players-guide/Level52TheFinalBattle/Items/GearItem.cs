using System.Data.Common;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.Items;

public class GearItem : InventoryItem
{
    public Attack GearAttack { get; init; }
    public int RoundsToActivate { get; set; }

    public GearItem(string name, Attack attack, int roundsToActivate = -1)
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

internal static class GearCreator
{
    public static GearItem CreateGearItem(GearItemEnum gearItem)
    {
        switch (gearItem)
        {
            case GearItemEnum.Sword:
                return new GearItem("Sword", AttackCreator.CreateAttack(AttackEnum.Slash));
            case GearItemEnum.Dagger:
                return new GearItem("Dagger", AttackCreator.CreateAttack(AttackEnum.Stab));
            case GearItemEnum.VinsBow:
                return new GearItem("Vin's bow", AttackCreator.CreateAttack(AttackEnum.QuickShot));
        }
        throw new NotImplementedException("Unkown gear type encoutered.");
    }
}
