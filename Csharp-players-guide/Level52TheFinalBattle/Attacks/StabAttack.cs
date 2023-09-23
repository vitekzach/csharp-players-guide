namespace Level52TheFinalBattle.Attacks;

public class StabAttack : Attack
{
    public StabAttack()
        : base("STAB") { }

    public override int DealDamage()
    {
        return 1;
    }
}
