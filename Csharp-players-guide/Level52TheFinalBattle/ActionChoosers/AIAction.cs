using System.Security;
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
        if (character.EquippedGear != null)
        {
            return CharacterMove.GearAttack;
        }
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
        var partyInventory = characterParty.Inventory;

        if (partyInventory.Count == 0)
            return -1;

        // choosing potions has priority over gear
        if (character.Hp < (float)character.HpInitial / 2)
        {
            int value = RandomNumberGenerator.Next(100);
            if (value <= 25)
                return 0;
        }

        List<GearItem> equippableGear = partyInventory.OfType<GearItem>().ToList<GearItem>();
        if (equippableGear.Count > 0 && character.EquippedGear == null)
        {
            Console.WriteLine("Console player sees gear");
            if (RandomNumberGenerator.Next(2) == 1)
            {
                Console.WriteLine("Equipping something...");
                int indexOfGear = RandomNumberGenerator.Next(equippableGear.Count);
                var gearToEquip = equippableGear[indexOfGear];
                return partyInventory.IndexOf(gearToEquip);
            }
        }
        return -1;
    }
}
