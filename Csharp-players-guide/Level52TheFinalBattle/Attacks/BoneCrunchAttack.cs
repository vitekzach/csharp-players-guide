namespace Level52TheFinalBattle.Attacks;

public class BoneCrunchhAttack : Attack
{
    private Random RandomNumberGenerator { get; } = new Random();

    public BoneCrunchhAttack()
        : base("BONE CRUNCH") { }

    public override int DealDamage()
    {
        return RandomNumberGenerator.Next(2);
    }
}
