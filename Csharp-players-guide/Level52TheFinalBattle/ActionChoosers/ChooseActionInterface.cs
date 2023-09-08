using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Characters;

namespace Level52TheFinalBattle.ActionChoosers;

public interface IChooseActionInterface
{
    public CharacterMoves ChooseAction(Character character);
}