using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.Characters;

public class SkeletonCharacter : AICharacter
{
    public SkeletonCharacter()
        : base("SKELETON", new AIAction(), new BoneCrunchhAttack()) { }
}
