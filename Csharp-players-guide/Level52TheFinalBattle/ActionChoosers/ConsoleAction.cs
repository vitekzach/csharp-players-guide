using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Items;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.ActionChoosers;

public class ConsoleAction : IChooseActionInterface
{
    public Attack ChooseAction(Character character)
    {
        int moveChoice = ConsoleHelpers.GetValidConsoleIntegerInputBasedOnListIndex<Attack>(
            "Move chosen: ",
            character.Moves,
            PrintAvailableMoves
        );

        return character.Moves[moveChoice];
    }

    private void PrintAvailableMoves(List<Attack> moves)
    {
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Choice,
            "Choose from the following moves:"
        );
        ConsoleHelpers.PrintChoicesFromList<Attack>(moves);
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

    public int ChooseInventoryItem(Character character, Battle battle)
    {
        Party characterParty = battle.GetPartyFor(character);

        if (characterParty.Inventory.Count == 0)
            return -1;

        List<InventoryItem> inventoryWithNothing = new List<InventoryItem>()
        {
            new InventoryItem("Do not use any item")
        };

        inventoryWithNothing.AddRange(characterParty.Inventory);

        int choice =
            ConsoleHelpers.GetValidConsoleIntegerInputBasedOnListIndex<InventoryItem>(
                "Item chosen: ",
                inventoryWithNothing,
                PrintInventoryChoices
            ) - 1;

        return choice;
    }

    private void PrintInventoryChoices(List<InventoryItem> items)
    {
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Choice, "Use item?");
        for (int i = 0; i < items.Count; i++)
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Choice,
                $" ({i + 1}) {items[i]}"
            );
    }
}
