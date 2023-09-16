using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Characters;

namespace Level52TheFinalBattle.Attacks;

public class AttackAction
{
    private Attack Attack { get; init; }
    private Character Target { get; init; }

    public AttackAction(Attack attack, Character target)
    {
        Attack = attack;
        Target = target;
    }

    public void Run()
    {
        int damage = Attack.DealDamage();
        int characterNewHp = Math.Clamp(Target.Hp - damage, 0, Target.HpInitial);

        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Attack,
            $"{Attack.Name} dealt {damage} damage to {Target.Name}. \n{Target.Name} is now at {characterNewHp}/{Target.HpInitial} HP."
        );

        Target.TakeDamage(damage);
    }
}
