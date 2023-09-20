using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;

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

    public int ChooseInventoryItem(Character character, Battle battle)
    {
        Party characterParty = battle.GetPartyFor(character);
        List<ConsumableItem> inventory = characterParty.Inventory;

        if (characterParty.Inventory.Count == 0)
            return -1;

        if (character.Hp < (float)character.HpInitial / 2)
        {
            int value = RandomNumberGenerator.Next(100);
            if (value <= 25)
                return 0;
        }

        return -1;
    }
}
