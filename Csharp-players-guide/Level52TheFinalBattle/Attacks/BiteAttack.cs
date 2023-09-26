using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.Attacks;

public class BiteAttack : Attack
{
    private Random RandomNumberGenerator { get; } = new Random();

    public BiteAttack()
        : base("BITE ATTACK", 1, DamageType.Normal, 100) { }
}
