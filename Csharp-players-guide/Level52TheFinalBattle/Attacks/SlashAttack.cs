namespace Level52TheFinalBattle.Attacks;

public class SlashAttack : Attack
{
    public SlashAttack()
        : base("SLASH") { }

    public override int DealDamage()
    {
        return 2;
    }
}
