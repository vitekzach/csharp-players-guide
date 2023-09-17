using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Characters;

namespace Level52TheFinalBattle.ActionChoosers;

public interface IChooseActionInterface
{
    public CharacterMove ChooseAction(Character character);

    public Character ChooseEnemyTarget(Character character, Battle battle);
}
