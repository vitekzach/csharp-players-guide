using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Interfaces;

namespace Level52TheFinalBattle.Items;

public class InventoryItem : IUsable
{
    public string Name { get; init; }

    public InventoryItem(string name)
    {
        Name = name;
    }

    public void Use(Character target, Party targetParty)
    {
        target.UseItem(this, targetParty);
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}
