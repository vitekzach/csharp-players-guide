using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Records;

namespace Level52TheFinalBattle.Attacks
{
    public abstract class Attack
    {
        public string Name { get; init; } = "DEFAULT";
        public int MaxDamage { get; init; }

        public DamageType DamageType { get; init; }
        private readonly Random randomNumberGenerator = new();

        private int _hitProbability;

        public int HitProbability
        {
            get => _hitProbability;
            init => _hitProbability = Math.Clamp(value, 0, 100);
        }

        public Attack(string name, int maxDamage, DamageType attackType, int hitProbability = -1)
        {
            Name = name;
            MaxDamage = maxDamage;
            HitProbability = hitProbability;
            DamageType = attackType;
        }

        public virtual int GetDamage()
        {
            int damageDealt = randomNumberGenerator.Next(100) < HitProbability ? MaxDamage : 0;
            return damageDealt;
        }

        public AttackData GetAttackData(Character attacker, Character target)
        {
            int damageDealt = GetDamage();
            return new AttackData(attacker, target, Name, damageDealt, DamageType);
        }
    }
}
