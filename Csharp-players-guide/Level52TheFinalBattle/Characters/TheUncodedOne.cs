using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle.Characters;

public class TheUncodedOne : Character
{
    public TheUncodedOne(
        IChooseActionInterface actionChooser,
        GearItem? startingGearItem = null,
        AttackModifier? defensiveAttackModifier = null
    )
        : base(
            "THE UNCODED ONE",
            actionChooser,
            new UnravelingAttack(),
            15,
            startingGearItem,
            defensiveAttackModifier
        ) { }
}
