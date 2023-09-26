using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle.Characters;

public class VinFletcher : Character
{
    public VinFletcher(
        IChooseActionInterface actionChooser,
        GearItem? startingGearItem = null,
        AttackModifier? defensiveAttackModifier = null
    )
        : base(
            "VIN FLETCHER",
            actionChooser,
            new PunchAttack(),
            15,
            startingGearItem,
            defensiveAttackModifier
        ) { }
}
