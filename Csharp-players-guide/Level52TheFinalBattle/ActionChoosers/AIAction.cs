using System.Security;
using System.Security.Cryptography.X509Certificates;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle.ActionChoosers;

public class AIAction : IChooseActionInterface
{
    private Random RandomNumberGenerator { get; } = new Random();

    public Attack ChooseAction(Character character)
    {
        if (character.EquippedGear.Any())
        {
            return character.EquippedGear.MaxBy(x => x.GearAttack.MaxDamage)!.GearAttack;
        }
        return character.MainAttack;
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
        if (character.Hp < (float)character.HpMax / 2)
        {
            int value = RandomNumberGenerator.Next(100);
            if (value <= 25)
                return 0;
        }

        List<GearItem> equippableGear = partyInventory.OfType<GearItem>().ToList<GearItem>();
        if (equippableGear.Count > 0 && character.EquippedGear == null)
        {
            if (RandomNumberGenerator.Next(2) == 1)
            {
                int indexOfGear = RandomNumberGenerator.Next(equippableGear.Count);
                var gearToEquip = equippableGear[indexOfGear];
                return partyInventory.IndexOf(gearToEquip);
            }
        }
        return -1;
    }
}
