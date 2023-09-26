using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.Attacks;

public class BoneCrunchhAttack : Attack
{
    private Random RandomNumberGenerator { get; } = new Random();

    public BoneCrunchhAttack()
        : base("BONE CRUNCH", 1, DamageType.Normal, 50) { }
}
