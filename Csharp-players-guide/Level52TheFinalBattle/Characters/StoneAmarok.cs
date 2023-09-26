using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle.Characters;

public class StoneAmarok : Character
{
    public StoneAmarok(
        IChooseActionInterface actionChooser,
        GearItem? startingGearItem = null,
        AttackModifier? defensiveAttackModifier = null
    )
        : base(
            "STONE AMAROK",
            actionChooser,
            new BiteAttack(),
            4,
            startingGearItem,
            defensiveAttackModifier
        ) { }

    private static string GetCharacterName(string characterType)
    {
        return ConsoleHelpers.GetValidConsoleStringInput("What is The True Programmer's name?");
    }
}
