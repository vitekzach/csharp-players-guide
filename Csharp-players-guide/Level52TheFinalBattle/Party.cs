using System.Collections;
using System.Dynamic;
using System.Net.Http.Headers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle;

public class Party
{
    public List<Character> Members { get; private set; }
    public PartyType Type { get; init; }
    public List<ConsumableItem> Inventory { get; private set; }

    public Party(List<Character> members, PartyType type, List<ConsumableItem> inventory)
    {
        Members = members;
        Type = type;
        Inventory = inventory;
    }
}
