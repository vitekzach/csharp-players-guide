using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle;

public class Party
{
    public List<Character> Members { get; private set; }
    public PartyType Type { get; init; }

    public Party(List<Character> members, PartyType type)
    {
        Members = members;
        Type = type;
    }
}