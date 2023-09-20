using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Interfaces;

namespace Level52TheFinalBattle.Items;

public class ConsumableItem : IConsumable
{
    public string Name { get; init; }

    public ConsumableItem(string name)
    {
        Name = name;
    }

    public void Consume(Character target)
    {
        target.ConsumeItem(this);
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}
