using System.Collections;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Records;

namespace Level52TheFinalBattle.Attacks;

public class Attack
{
    public string Name { get; init; } = "DEFAULT";
    public int MaxDamage { get; init; }

    public DamageType DamageType { get; init; }
    private readonly Random randomNumberGenerator = new();
    private Func<int> GetDamage { get; init; }
    private int _usageCount;

    public int? HitProbability { get; init; }

    internal Attack(
        string name,
        int maxDamage,
        DamageType attackType,
        AttackDamageGenerationEnum damageGenerationMechanism
    )
    {
        _usageCount = 0;
        Name = name;
        MaxDamage = maxDamage;
        DamageType = attackType;
        GetDamage = damageGenerationMechanism switch
        {
            AttackDamageGenerationEnum.ProbabilityBased => GetDamageWithHitProbability,
            AttackDamageGenerationEnum.RandomNumberBased => GetDamageRandomNumber,
            AttackDamageGenerationEnum.UsageCountBased => GetDamageUsageTimeBased,
            _
                => throw new NotImplementedException(
                    "Unkown damage generation mechanism encountered."
                )
        };
    }

    public Attack(string name, int maxDamage, DamageType attackType, int hitProbability)
        : this(name, maxDamage, attackType, AttackDamageGenerationEnum.ProbabilityBased)
    {
        HitProbability = hitProbability;
    }

    private int GetDamageWithHitProbability()
    {
        int damageDealt = randomNumberGenerator.Next(100) < HitProbability ? MaxDamage : 0;
        return damageDealt;
    }

    private int GetDamageRandomNumber()
    {
        return randomNumberGenerator.Next(MaxDamage + 1);
    }

    private int GetDamageUsageTimeBased()
    {
        _usageCount++;
        Console.WriteLine($"Usage count: {_usageCount}");

        bool used3 = _usageCount % 3 == 0;
        bool used5 = _usageCount % 5 == 0;
        if (used3 && used5)
            return MaxDamage;
        if (used3 || used5)
            return 2;
        return 1;
    }

    public AttackData GetAttackData(Character attacker, Character target)
    {
        int damageDealt = GetDamage();
        return new AttackData(attacker, target, Name, damageDealt, DamageType);
    }

    public override string ToString()
    {
        return $"{Name} (max damage {MaxDamage})";
    }
}

internal static class AttackCreator
{
    internal static Attack CreateAttack(AttackEnum attack)
    {
        return attack switch
        {
            AttackEnum.DoNothing
                => new Attack(
                    "DO NOTHING",
                    0,
                    DamageType.NoDamage,
                    AttackDamageGenerationEnum.RandomNumberBased
                ),
            AttackEnum.Bite => new Attack("BITE ATTACK", 8, DamageType.Normal, 100),
            AttackEnum.BoneCrunch => new Attack("BONE CRUNCH", 8, DamageType.Normal, 50),
            AttackEnum.Punch => new Attack("PUNCH", 1, DamageType.Normal, 50),
            AttackEnum.QuickShot => new Attack("QUICK SHOT", 3, DamageType.Normal, 50),
            AttackEnum.Slash => new Attack("SLASH", 2, DamageType.Normal, 100),
            AttackEnum.Stab => new Attack("STAB", 1, DamageType.Normal, 100),
            AttackEnum.Unraveling
                => new Attack(
                    "UNRAVELING ATTACK",
                   10,
                    DamageType.Decoding,
                    AttackDamageGenerationEnum.RandomNumberBased
                ),
            AttackEnum.CannonShot
                => new Attack(
                    "CANNON SHOT",
                    5,
                    DamageType.Normal,
                    AttackDamageGenerationEnum.UsageCountBased
                ),
            _ => throw new NotImplementedException("Unknown attack type enoucntered.")
        };
    }
}
