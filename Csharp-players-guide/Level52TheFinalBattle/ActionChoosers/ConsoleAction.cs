using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Enums;

namespace Level52TheFinalBattle.ActionChoosers;

public class ConsoleAction : IChooseActionInterface
{
    public CharacterMoves ChooseAction(Character character)
    {
        int moveChoice = ConsoleHelpers.GetValidConsoleIntegerInputBasedOnListIndex<CharacterMoves>(
            "Move chosen: ",
            character.Moves,
            PrintAvailableMoves
        );

        return character.Moves[moveChoice];
    }

    private void PrintAvailableMoves(List<CharacterMoves> moves)
    {
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Choice,
            "Choose from the following moves:"
        );
        ConsoleHelpers.PrintChoicesFromList<CharacterMoves>(moves);

        // for (int i = 0; i < character.Moves.Count; i++)
        // {
        //     ConsoleHelpers.WriteLineWithColoredConsole(
        //         MessageType.Choice,
        //         $" ({i + 1}) {character.Moves[i]}"
        //     );
        // }
    }
}
