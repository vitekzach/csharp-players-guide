using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle.ActionChoosers;

public interface IChooseActionInterface
{
    public Attack ChooseAction(Character character);

    public Character ChooseEnemyTarget(Character character, Battle battle);

    public int ChooseInventoryItem(Character character, Battle Battle);
}
