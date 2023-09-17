using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.Characters;

public class SkeletonCharacter : Character
{
    public SkeletonCharacter(IChooseActionInterface actionChooser)
        : base("SKELETON", actionChooser, new BoneCrunchhAttack(), 5) { }
}
