using Level52TheFinalBattle.Interfaces;

namespace Level52TheFinalBattle.Attacks;

public class HitOrMissProbabilityDamageDealer : IAttack
{
    public int DealDamage(int maxDamage, int hitProbability)
    {
        Random randomGenerator = new Random();
        if (randomGenerator.Next(maxDamage) < hitProbability)
            return maxDamage;
        return 0;
    }
}

public class HitOrMissMaxOrLowerDamageDealer : IAttack
{
    public int DealDamage(int maxDamage, int hitProbability)
    {
        Random randomGenerator = new Random();
        if (randomGenerator.Next(maxDamage) < hitProbability)
            return maxDamage;
        return 0;
    }
}
