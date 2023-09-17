using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.Characters;

public class TheTrueProgrammer : Character
{
    public TheTrueProgrammer(IChooseActionInterface actionChooser)
        : base(GetCharacterName("The True Programmer"), actionChooser, new PunchAttack(), 25) { }

    private static string GetCharacterName(string characterType)
    {
        return ConsoleHelpers.GetValidConsoleStringInput("What is The True Programmer's name?");
    }
}
