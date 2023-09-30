using System.Reflection.Metadata.Ecma335;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Records;

namespace Level52TheFinalBattle.Attacks;

public class AttackModifier
{
    public string Name { get; init; }
    public int DamageModifier { get; init; }

    public DamageType ModifyingDamageType { get; init; }

    public AttackModifier(string name, int damageModifier, DamageType modyfingDamageType)
    {
        Name = name;
        DamageModifier = damageModifier;
        ModifyingDamageType = modyfingDamageType;
    }

    public AttackData ModifyAttack(AttackData attack)
    {
        if (attack.DamageType == ModifyingDamageType || ModifyingDamageType == DamageType.All)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Attack,
                $"{Name} modified the attack strength by {DamageModifier} point."
            );
            return attack with
            {
                Damage = Math.Clamp(attack.Damage + DamageModifier, 0, int.MaxValue)
            };
        }
        return attack;
    }
}

internal static class AttackModifierCreator
{
    internal static AttackModifier CreateDefensiveAttackModifier(
        DefensiveAttackModifierEnum defensiveAttackModifier
    )
    {
        return defensiveAttackModifier switch
        {
            DefensiveAttackModifierEnum.ObjectSight
                => new AttackModifier("Object Sight", -2, DamageType.Decoding),
            DefensiveAttackModifierEnum.StoneArmor
                => new AttackModifier("Stone Armor", -1, DamageType.Normal),
            DefensiveAttackModifierEnum.BinaryHelm
                => new AttackModifier("Binary Helm", -1, DamageType.All),
            _ => throw new NotImplementedException("Unkown defensive attack modifier encountered.")
        };
    }

    internal static AttackModifier CreateOffensiveAttackModifier(
        OffensiveAttackModifierEnum offensiveAttackModifier
    )
    {
        return offensiveAttackModifier switch
        {
            OffensiveAttackModifierEnum.CodersAdvantage
                => new AttackModifier("Coder's advatage", 2, DamageType.Normal),
            _ => throw new NotImplementedException("Unkown offensive attack modifier encountered.")
        };
    }
}
