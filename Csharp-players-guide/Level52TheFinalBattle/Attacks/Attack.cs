using System.Runtime;
using System.Runtime.InteropServices;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Interfaces;
using Level52TheFinalBattle.Records;

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

    public virtual int GetDamage()
    {
        int damageDealt;

        if (randomNumberGenerator.Next(100) < HitProbability)
            damageDealt = MaxDamage;
        else
            damageDealt = 0;

        return damageDealt;
    }

    public AttackData GetAttackData(Character attacker, Character target)
    {
        int damageDealt = GetDamage();
        return new AttackData(attacker, target, Name, damageDealt);
    }
}
