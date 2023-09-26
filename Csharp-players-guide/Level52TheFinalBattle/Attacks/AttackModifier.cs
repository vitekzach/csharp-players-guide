using Level52TheFinalBattle.Records;

namespace Level52TheFinalBattle.Attacks;

public class AttackModifier
{
    public string Name { get; init; }
    public int DamageModifier { get; init; }

    public AttackModifier(string name, int damageModifier)
    {
        Name = name;
        DamageModifier = damageModifier;
    }

    public AttackData ModifyAttack(AttackData attack)
    {
        return attack with { Damage = attack.Damage + DamageModifier };
    }
}
