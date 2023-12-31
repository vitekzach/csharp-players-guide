using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;
using Level52TheFinalBattle.Records;

namespace Level52TheFinalBattle.Attacks;

public class AttackAction
{
    private Attack Attack { get; init; }
    private Character Target { get; init; }
    private Character _attacker;

    public AttackAction(Attack attack, Character attacker, Character target)
    {
        Attack = attack;
        _attacker = attacker;
        Target = target;
    }

    public void Run()
    {
        AttackData attackData = Attack.GetAttackData(_attacker, Target);

        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Normal,
            $"{_attacker.Name} used {Attack.Name} against {Target.Name}."
        );

        if (attackData.Damage == 0)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Attack,
                $"{_attacker.Name} MISSED!"
            );
            return;
        }

        int characterHpBefore = Target.Hp;

        if (_attacker.OffensiveAttackModifier != null)
            attackData = _attacker.OffensiveAttackModifier.ModifyAttack(attackData);

        foreach (GearItem gearItem in _attacker.EquippedGear)
        {
            if (gearItem.OffensiveAttackModifier != null)
                attackData = gearItem.OffensiveAttackModifier.ModifyAttack(attackData);
        }

        Target.GetAttacked(attackData);

        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Attack,
            $"{Attack.Name} dealt {characterHpBefore - Target.Hp} damage to {Target.Name}."
        );
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Attack,
            $"{Target.Name} is now at {Target.Hp}/{Target.HpMax} HP."
        );
    }
}
