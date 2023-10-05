using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;
using Level52TheFinalBattle.Records;

namespace Level52TheFinalBattle.Characters;

public class Character
{
    public event Action<Character, Character>? CharacterDied;
    public string Name { get; init; }
    public List<Attack> Moves { get; private set; }
    private IChooseActionInterface ActionChooser { get; init; }

    public Attack MainAttack { get; init; }

    public List<GearItem> EquippedGear { get; set; }

    public AttackModifier? DefensiveAttackModifier { get; init; }
    public AttackModifier? OffensiveAttackModifier { get; init; }

    public int HpMax { get; init; }

    public int XP { get; init; }

    private int _hp;
    public int Hp
    {
        get => _hp;
        set => _hp = Math.Clamp(value, 0, HpMax);
    }

    public Character(
        string name,
        IChooseActionInterface actionChooser,
        Attack attack,
        int hpInitial,
        int xP,
        List<GearItem>? startingGearItem = null,
        AttackModifier? defensiveAttackModifier = null,
        AttackModifier? offensiveAttackModifier = null
    )
    {
        Name = name;
        ActionChooser = actionChooser;
        MainAttack = attack;
        HpMax = hpInitial;
        Hp = hpInitial;
        XP = xP;
        EquippedGear = startingGearItem ?? new List<GearItem>();
        if (defensiveAttackModifier != null)
            DefensiveAttackModifier = defensiveAttackModifier;
        if (offensiveAttackModifier != null)
            OffensiveAttackModifier = offensiveAttackModifier;
        Moves = CreateMoveChoiceList();
    }

    public virtual void TakeTurn(Battle battle)
    {
        foreach (GearItem gearItem in EquippedGear)
        {
            if (gearItem.RoundsToActivate == 1)
            {
                Moves = CreateMoveChoiceList();
                gearItem.RoundsToActivate = -1;
            }

            if (gearItem.RoundsToActivate > 1)
                gearItem.RoundsToActivate--;
        }

        Party characterParty = battle.GetPartyFor(this);

        int inventoryChoice = ActionChooser.ChooseInventoryItem(this, battle);
        if (inventoryChoice >= 0)
        {
            InventoryItem item = characterParty.Inventory[inventoryChoice];
            characterParty.Inventory.RemoveAt(inventoryChoice);
            UseItem(item, characterParty);
        }

        Attack move = ActionChooser.ChooseAction(this);
        if (move.DamageType == DamageType.NoDamage)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Attack, $"{Name} did nothing.");
            return;
        }
        Character chosenEnemy = ActionChooser.ChooseEnemyTarget(this, battle);

        AttackAction attackAction = new AttackAction(move, this, chosenEnemy);
        attackAction.Run();
    }

    public void GetAttacked(AttackData attackData)
    {
        if (DefensiveAttackModifier != null)
        {
            attackData = DefensiveAttackModifier.ModifyAttack(attackData);
        }
        foreach (GearItem gearItem in EquippedGear)
        {
            if (gearItem.DefensiveAttackModifier != null)
                attackData = gearItem.DefensiveAttackModifier.ModifyAttack(attackData);
        }

        Hp -= attackData.Damage;
        if (Hp == 0)
        {
            CharacterDied?.Invoke(this, attackData.attacker);
        }
    }

    public void DoNothing() { }

    public void TakeHealing(int healing)
    {
        Hp = Math.Clamp(Hp + healing, 0, HpMax);
    }

    public override string ToString()
    {
        string suffix = "";
        var equippedGearItemNamesEnumerable = EquippedGear.Select(x => x.Name);
        string equippedGearItemNames = string.Join(',', equippedGearItemNamesEnumerable);
        if (equippedGearItemNames != "")
            suffix += $" (wearing {equippedGearItemNames})";
        return $"{Name}{suffix}";
    }

    public void UseItem(InventoryItem item, Party party)
    {
        switch (item)
        {
            case HealthPotionItem h:
                TakeHealing(h.HealingPower);
                ConsoleHelpers.WriteLineWithColoredConsole(
                    MessageType.Item,
                    $"{Name} consumed {item}."
                );
                break;
            case GearItem g:
                ConsoleHelpers.WriteLineWithColoredConsole(
                    MessageType.Item,
                    $"{Name} equipped {g.Name}."
                );
                EquippedGear.Add(g);
                Moves = CreateMoveChoiceList();
                break;
        }
    }

    private List<Attack> CreateMoveChoiceList()
    {
        List<Attack> ListOfMoves = new List<Attack>
        {
            AttackCreator.CreateAttack(AttackEnum.DoNothing),
            MainAttack
        };
        foreach (GearItem gearItem in EquippedGear)
            if (
                gearItem.RoundsToActivate < 1
                && gearItem.GearAttack.DamageType != DamageType.NoDamage
            )
                ListOfMoves.Add(gearItem.GearAttack);
        return ListOfMoves;
    }
}

internal static class CharacterCreator
{
    internal static Character CreateHeroCharacter(
        HeroCharacter character,
        ActionChooserEnum actionChooserType
    )
    {
        var actionChooser = ActionChooserCreator.CreateActionChooser(actionChooserType);

        switch (character)
        {
            case HeroCharacter.TheTrueProgrammer:
                string name = GetCharacterName(HeroCharacter.TheTrueProgrammer);
                return new Character(
                    name,
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.Punch),
                    25,
                    50,
                    new List<GearItem>
                    {
                        GearCreator.CreateGearItem(GearItemEnum.Sword),
                        GearCreator.CreateGearItem(GearItemEnum.BinaryHelm)
                    },
                    AttackModifierCreator.CreateDefensiveAttackModifier(
                        DefensiveAttackModifierEnum.ObjectSight
                    ),
                    AttackModifierCreator.CreateOffensiveAttackModifier(
                        OffensiveAttackModifierEnum.CodersAdvantage
                    )
                );
            case HeroCharacter.VinFletcher:
                return new Character(
                    "VIN FLETCHER",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.Punch),
                    15,
                    25,
                    new List<GearItem> { GearCreator.CreateGearItem(GearItemEnum.VinsBow) }
                );
            case HeroCharacter.Mylara:
                return new Character(
                    "MYLARA",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.CannonShot),
                    15,
                    25
                );
            case HeroCharacter.Skorin:
                return new Character(
                    "SKORIN",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.CannonShot),
                    15,
                    25
                );
        }

        throw new NotImplementedException("Unknown Hero enountered.");
    }

    internal static Character CreateMonsterCharacter(
        MonsterCharacter character,
        ActionChooserEnum actionChooserType
    )
    {
        var actionChooser = ActionChooserCreator.CreateActionChooser(actionChooserType);

        switch (character)
        {
            case MonsterCharacter.Skeleton:
                return new Character(
                    "SKELETON",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.BoneCrunch),
                    5,
                    1
                );
            case MonsterCharacter.SkeletonWithDagger:
                return new Character(
                    "SKELETON",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.BoneCrunch),
                    5,
                    3,
                    new List<GearItem> { GearCreator.CreateGearItem(GearItemEnum.Dagger) }
                );
            case MonsterCharacter.StoneAmarok:
                return new Character(
                    "STONE AMAROK",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.Bite),
                    4,
                    5,
                    null,
                    AttackModifierCreator.CreateDefensiveAttackModifier(
                        DefensiveAttackModifierEnum.StoneArmor
                    )
                );
            case MonsterCharacter.TheUncodedOne:
                return new Character(
                    "THE UNCODED ONE",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.Unraveling),
                    15,
                    25
                );
        }

        throw new NotImplementedException("Unknown Hero enountered.");
    }

    private static string GetCharacterName(HeroCharacter character)
    {
        return ConsoleHelpers.GetValidConsoleStringInput($"What is {character}'s name?");
    }
}
