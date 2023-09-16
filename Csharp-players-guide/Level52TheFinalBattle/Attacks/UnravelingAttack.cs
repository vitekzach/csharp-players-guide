namespace Level52TheFinalBattle.Attacks;

public class UnravelingAttack : Attack
{
    private Random RandomNumberGenerator { get; } = new Random();

    public UnravelingAttack()
        : base("UNRAVELING ATTACK") { }

    public override int DealDamage()
    {
        return RandomNumberGenerator.Next(3);
    }
}
