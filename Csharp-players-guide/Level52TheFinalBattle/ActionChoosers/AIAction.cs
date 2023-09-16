using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle.ActionChoosers;

public class AIAction : IChooseActionInterface
{
    public CharacterMoves ChooseAction(Character character)
    {
        return CharacterMoves.Attack;
    }
}
