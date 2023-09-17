using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle.ActionChoosers;

public class AIAction : IChooseActionInterface
{
    private Random RandomNumberGenerator { get; } = new Random();

    public CharacterMove ChooseAction(Character character)
    {
        return CharacterMove.Attack;
    }

    public Character ChooseEnemyTarget(Character character, Battle battle)
    {
        Party enemyParty = battle.GetEnemyPartyFor(character);

        int enemyIndexChoice = PickRandomPartyMember(enemyParty);

        return enemyParty.Members[enemyIndexChoice];
    }

    private int PickRandomPartyMember(Party party)
    {
        return RandomNumberGenerator.Next(party.Members.Count);
    }
}
