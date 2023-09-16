using System.Runtime;
using Level52TheFinalBattle.Characters;

namespace Level52TheFinalBattle.Attacks;

public abstract class Attack
{
    public string Name { get; init; } = "DEFAULT";

    public Attack(string name)
    {
        Name = name;
    }

    public abstract int DealDamage();
}
