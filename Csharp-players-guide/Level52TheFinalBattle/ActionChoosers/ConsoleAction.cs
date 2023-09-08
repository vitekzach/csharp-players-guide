using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Enums;


namespace Level52TheFinalBattle.ActionChoosers;


public class ConsoleAction: IChooseActionInterface
{
    public CharacterMoves ChooseAction(Character character)
    {
        while (true)
        {
            PrintAvailableMoves(character);
            int moveChoice = ConsoleHelpers.GetValidConsoleIntegerInput("Move chosen: ") - 1;
            if (moveChoice >= 0 && moveChoice < character.Moves.Count)
            {
                return character.Moves[moveChoice];
            }
            ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Error, "You need to input a number form the list.");
        }

    }

    private void PrintAvailableMoves(Character character)
    {

        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Choice, "Choose from the following moves:");
        for (int i = 0; i < character.Moves.Count; i++)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Choice, $" ({i+1}) {character.Moves[i]}");
        }
    }
}