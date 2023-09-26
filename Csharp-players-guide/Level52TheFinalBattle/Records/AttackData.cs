using System.Runtime.Serialization;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.Records;

public record AttackData(
    Character attacker,
    Character target,
    string AttackName,
    int Damage,
    DamageType DamageType
);
