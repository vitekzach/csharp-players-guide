using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.Characters;

public class TheTrueProgrammer : Character
{
    public TheTrueProgrammer(string name)
        : base(name, new ConsoleAction(), new PunchAttack()) { }

    public TheTrueProgrammer()
        : this(GetCharacterName("The True Programmer")) { }

    private static string GetCharacterName(string characterType)
    {
        return ConsoleHelpers.GetValidConsoleStringInput("What is The True Programmer's name?");
    }
}
