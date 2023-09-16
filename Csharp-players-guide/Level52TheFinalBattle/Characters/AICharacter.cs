using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;

namespace Level52TheFinalBattle.Characters;

public class AICharacter : Character
{
    public AICharacter(
        string name,
        IChooseActionInterface chooseActionInterface,
        Attack attack,
        int hpInitial
    )
        : base(name, chooseActionInterface, attack, hpInitial) { }

    public override void TakeTurn(Battle battle)
    {
        AiTakeTurn(battle);
    }
}
