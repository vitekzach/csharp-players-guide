using System.Runtime;
using System.Runtime.InteropServices;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Interfaces;

namespace Level52TheFinalBattle.Attacks;

public abstract class Attack
{
    public string Name { get; init; } = "DEFAULT";
    public int MaxDamage { get; init; }

    private Random randomNumberGenerator = new Random();

    private int _hitProbability;

    public int HitProbability
    {
        get => _hitProbability;
        init => _hitProbability = Math.Clamp(value, 0, 100);
    }

    public Attack(string name, int maxDamage, int hitProbability = -1)
    {
        Name = name;
        MaxDamage = maxDamage;
        HitProbability = hitProbability;
    }

    public virtual int DealDamage()
    {
        if (randomNumberGenerator.Next(100) < HitProbability)
            return MaxDamage;
        return 0;
    }
}
