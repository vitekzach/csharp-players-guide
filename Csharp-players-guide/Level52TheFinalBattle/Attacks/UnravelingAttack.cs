using System.Security;

namespace Level52TheFinalBattle.Attacks;

public class UnravelingAttack : Attack
{
    private Random RandomNumberGenerator { get; } = new Random();

    public UnravelingAttack()
        : base("UNRAVELING ATTACK", 2, -1) { }

    public override int GetDamage()
    {
        return RandomNumberGenerator.Next(MaxDamage + 1);
    }
}
