using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.ActionChoosers;

public class ConsoleAction : IChooseActionInterface
{
    public CharacterMove ChooseAction(Character character)
    {
        int moveChoice = ConsoleHelpers.GetValidConsoleIntegerInputBasedOnListIndex<CharacterMove>(
            "Move chosen: ",
            character.Moves,
            PrintAvailableMoves
        );

        return character.Moves[moveChoice];
    }

    private void PrintAvailableMoves(List<CharacterMove> moves)
    {
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Choice,
            "Choose from the following moves:"
        );
        ConsoleHelpers.PrintChoicesFromList<CharacterMove>(moves);

        // for (int i = 0; i < character.Moves.Count; i++)
        // {
        //     ConsoleHelpers.WriteLineWithColoredConsole(
        //         MessageType.Choice,
        //         $" ({i + 1}) {character.Moves[i]}"
        //     );
        // }
    }

    public Character ChooseEnemyTarget(Character character, Battle battle)
    {
        Party enemyParty = battle.GetEnemyPartyFor(character);
        int enemyIndexChoice =
            ConsoleHelpers.GetValidConsoleIntegerInputBasedOnListIndex<Character>(
                "Enemy chosen: ",
                enemyParty.Members,
                ConsoleHelpers.PrintChoicesFromList<Character>
            );

        return enemyParty.Members[enemyIndexChoice];
    }
}
