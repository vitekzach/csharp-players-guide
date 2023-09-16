namespace Level52TheFinalBattle.Attacks;

public class PunchAttack : Attack
{
    public PunchAttack()
        : base("PUNCH") { }

    public override int DealDamage()
    {
        return 1;
    }
}
