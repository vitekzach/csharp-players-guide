using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.Characters;

public class TheUncodedOne : AICharacter
{
    public TheUncodedOne()
        : base("THE UNCODED ONE", new AIAction(), new UnravelingAttack(), 15) { }
}
