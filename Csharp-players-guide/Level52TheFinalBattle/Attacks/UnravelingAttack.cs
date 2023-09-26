using System.Security;
using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.Attacks;

public class UnravelingAttack : Attack
{
    private Random RandomNumberGenerator { get; } = new Random();

    public UnravelingAttack()
        : base("UNRAVELING ATTACK", 4, DamageType.Decoding, -1) { }

    public override int GetDamage()
    {
        return RandomNumberGenerator.Next(MaxDamage + 1);
    }
}
