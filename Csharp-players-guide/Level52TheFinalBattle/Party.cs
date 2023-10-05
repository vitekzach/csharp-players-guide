using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle;

public class Party
{
    public List<Character> Members { get; private set; }
    public PartyType Type { get; init; }
    public List<InventoryItem> Inventory { get; private set; }

    public int GainedXP { get; set; }

    public Party(
        List<Character> members,
        PartyType type,
        List<InventoryItem> inventory,
        int initialXP = 0
    )
    {
        Members = members;
        Type = type;
        Inventory = inventory;
        GainedXP = initialXP;
    }

    public override string ToString()
    {
        return $"{Type} ({GainedXP} XP)";
    }
}
