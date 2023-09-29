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

    public int? HitProbability { get; init; }

    public Attack(string name, int maxDamage, DamageType attackType)
    {
        Name = name;
        MaxDamage = maxDamage;
        DamageType = attackType;
        GetDamage = GetDamageRandomNumber;
    }

    public Attack(string name, int maxDamage, DamageType attackType, int hitProbability)
        : this(name, maxDamage, attackType)
    {
        HitProbability = hitProbability;
        GetDamage = GetDamageWithHitProbability;
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

    public AttackData GetAttackData(Character attacker, Character target)
    {
        int damageDealt = GetDamage();
        return new AttackData(attacker, target, Name, damageDealt, DamageType);
    }
}

internal static class AttackCreator
{
    internal static Attack CreateAttack(AttackEnum attack)
    {
        switch (attack)
        {
            case AttackEnum.Bite:
                return new Attack("BITE ATTACK", 1, DamageType.Normal, 100);
            case AttackEnum.BoneCrunch:
                return new Attack("BONE CRUNCH", 1, DamageType.Normal, 50);
            case AttackEnum.Punch:
                return new Attack("PUNCH", 1, DamageType.Normal, 50);
            case AttackEnum.QuickShot:
                return new Attack("QUICK SHOT", 3, DamageType.Normal, 50);
            case AttackEnum.Slash:
                return new Attack("SLASH", 2, DamageType.Normal, 100);
            case AttackEnum.Stab:
                return new Attack("STAB", 1, DamageType.Normal, 100);
            case AttackEnum.Unraveling:
                return new Attack("UNRAVELING ATTACK", 4, DamageType.Decoding);
        }
        throw new NotImplementedException("Unknown attack type enoucntered.");
    }
}
