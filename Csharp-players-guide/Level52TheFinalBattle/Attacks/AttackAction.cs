using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

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
        int damage = Attack.DealDamage();

        if (damage == 0)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Attack,
                $"{_attacker.Name} MISSED!"
            );
            return;
        }

        int characterNewHp = Math.Clamp(Target.Hp - damage, 0, Target.HpInitial);

        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Attack,
            $"{Attack.Name} dealt {damage} damage to {Target.Name}."
        );
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Attack,
            $"{Target.Name} is now at {characterNewHp}/{Target.HpInitial} HP."
        );

        Target.TakeDamage(damage);
    }
}
