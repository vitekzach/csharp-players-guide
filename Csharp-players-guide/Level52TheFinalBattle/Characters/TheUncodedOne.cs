using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.Characters;

public class TheUncodedOne : Character
{
    public TheUncodedOne(IChooseActionInterface actionChooser)
        : base("THE UNCODED ONE", actionChooser, new UnravelingAttack(), 15) { }
}
