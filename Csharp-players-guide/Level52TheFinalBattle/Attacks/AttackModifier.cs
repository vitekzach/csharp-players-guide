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
        if (attack.DamageType == ModifyingDamageType)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Attack,
                $"{Name} modified the attack strength by {DamageModifier} point."
            );
            return attack with { Damage = attack.Damage + DamageModifier };
        }
        return attack;
    }
}
