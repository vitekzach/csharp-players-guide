using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle.Characters;

public class SkeletonCharacter : Character
{
    public SkeletonCharacter(
        IChooseActionInterface actionChooser,
        GearItem? startingGearItem = null
    )
        : base("SKELETON", actionChooser, new BoneCrunchhAttack(), 5, startingGearItem) { }
}
