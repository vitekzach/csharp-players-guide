using System.Data.Common;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.Items;

public class GearItem : InventoryItem
{
    public Attack GearAttack { get; init; }
    public int RoundsToActivate { get; set; }

    public AttackModifier? DefensiveAttackModifier { get; private set; }
    public AttackModifier? OffensiveAttackModifier { get; private set; }

    public GearItem(string name, Attack attack, int roundsToActivate = -1)
        : base(name)
    {
        GearAttack = attack;
        RoundsToActivate = roundsToActivate;
    }

    internal GearItem(
        string name,
        Attack attack,
        DefensiveAttackModifierEnum defensiveAttackModifier,
        OffensiveAttackModifierEnum offensiveAttackModifier,
        int roundsToActivate = -1
    )
        : this(name, attack, roundsToActivate)
    {
        DefensiveAttackModifier = AttackModifierCreator.CreateDefensiveAttackModifier(
            defensiveAttackModifier
        );
        OffensiveAttackModifier = AttackModifierCreator.CreateOffensiveAttackModifier(
            offensiveAttackModifier
        );
    }

    internal GearItem(
        string name,
        Attack attack,
        DefensiveAttackModifierEnum defensiveAttackModifier,
        int roundsToActivate = -1
    )
        : this(name, attack, roundsToActivate)
    {
        DefensiveAttackModifier = AttackModifierCreator.CreateDefensiveAttackModifier(
            defensiveAttackModifier
        );
    }

    internal GearItem(
        string name,
        Attack attack,
        OffensiveAttackModifierEnum offensiveAttackModifier,
        int roundsToActivate = -1
    )
        : this(name, attack, roundsToActivate)
    {
        OffensiveAttackModifier = AttackModifierCreator.CreateOffensiveAttackModifier(
            offensiveAttackModifier
        );
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
        return gearItem switch
        {
            GearItemEnum.Sword
                => new GearItem("Sword", AttackCreator.CreateAttack(AttackEnum.Slash)),
            GearItemEnum.Dagger
                => new GearItem("Dagger", AttackCreator.CreateAttack(AttackEnum.Stab)),
            GearItemEnum.VinsBow
                => new GearItem("Vin's bow", AttackCreator.CreateAttack(AttackEnum.QuickShot)),
            GearItemEnum.BinaryHelm
                => new GearItem(
                    "Binary Helm",
                    AttackCreator.CreateAttack(AttackEnum.DoNothing),
                    DefensiveAttackModifierEnum.BinaryHelm
                ),
            _ => throw new NotImplementedException("Unkown gear type encoutered.")
        };
    }
}
