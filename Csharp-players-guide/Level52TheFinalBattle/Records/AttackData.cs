using System.Runtime.Serialization;
using Level52TheFinalBattle.Characters;

namespace Level52TheFinalBattle.Records;

public record AttackData(Character attacker, Character target, string AttackName, int Damage);
