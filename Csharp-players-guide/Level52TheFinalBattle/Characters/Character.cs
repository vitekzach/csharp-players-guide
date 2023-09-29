using System.Diagnostics.Tracing;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;
using Level52TheFinalBattle.Records;

namespace Level52TheFinalBattle.Characters;

public class Character
{
    public event Action<Character>? CharacterDied;
    public string Name { get; init; }
    public List<CharacterMove> Moves { get; private set; }
    private IChooseActionInterface ActionChooser { get; init; }

    public Attack Attack { get; init; }

    public GearItem? EquippedGear { get; set; }

    public AttackModifier? DefensiveAttackModifier { get; init; }

    public int HpMax { get; init; }

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
        GearItem? startingGearItem = null,
        AttackModifier? defensiveAttackModifier = null
    )
    {
        Name = name;
        Moves = new List<CharacterMove>() { CharacterMove.Nothing, CharacterMove.Attack };
        ActionChooser = actionChooser;
        Attack = attack;
        HpMax = hpInitial;
        Hp = hpInitial;
        EquippedGear = startingGearItem;
        if (startingGearItem != null)
            Moves.Add(CharacterMove.GearAttack);
        if (defensiveAttackModifier != null)
            DefensiveAttackModifier = defensiveAttackModifier;
    }

    public virtual void TakeTurn(Battle battle)
    {
        if (EquippedGear?.RoundsToActivate == 1)
        {
            Moves.Add(CharacterMove.GearAttack);
            EquippedGear.RoundsToActivate = -1;
        }

        if (EquippedGear?.RoundsToActivate > 1)
            EquippedGear.RoundsToActivate--;

        Party characterParty = battle.GetPartyFor(this);

        int inventoryChoice = ActionChooser.ChooseInventoryItem(this, battle);
        if (inventoryChoice >= 0)
        {
            InventoryItem item = characterParty.Inventory[inventoryChoice];
            characterParty.Inventory.RemoveAt(inventoryChoice);
            UseItem(item, characterParty);
        }

        CharacterMove move = ActionChooser.ChooseAction(this);
        if (move == CharacterMove.Attack || move == CharacterMove.GearAttack)
        {
            Attack attack;
            if (move == CharacterMove.Attack)
            {
                attack = Attack;
            }
            else if (move == CharacterMove.GearAttack)
            {
                attack = EquippedGear!.GearAttack;
            }
            else
            {
                throw new NotImplementedException("What is this move?");
            }
            Character chosenEnemy = ActionChooser.ChooseEnemyTarget(this, battle);

            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Normal,
                $"{Name} used {attack.Name} against {chosenEnemy.Name}."
            );
            AttackAction attackAction = new AttackAction(attack, this, chosenEnemy);
            attackAction.Run();
        }
        else
            Console.WriteLine($"{Name} did {move}.");
    }

    public void GetAttacked(AttackData attackData)
    {
        if (DefensiveAttackModifier != null)
        {
            attackData = DefensiveAttackModifier.ModifyAttack(attackData);
        }
        Hp -= attackData.Damage;
        if (Hp == 0)
        {
            CharacterDied?.Invoke(this);
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
        if (EquippedGear != null)
            suffix += $" (with {EquippedGear.Name})";
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
                if (EquippedGear != null)
                {
                    ConsoleHelpers.WriteLineWithColoredConsole(
                        MessageType.Item,
                        $"Putting {EquippedGear.Name} back to team inventory."
                    );
                    party.Inventory.Add(EquippedGear);
                    EquippedGear = null;
                }
                ConsoleHelpers.WriteLineWithColoredConsole(
                    MessageType.Item,
                    $"{Name} equipped {g.Name}."
                );
                EquippedGear = g;
                break;
        }
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
                    GearCreator.CreateGearItem(GearItemEnum.Sword),
                    AttackModifierCreator.CreateDefensiveAttackModifier(
                        DefensiveAttackModifierEnum.ObjectSight
                    )
                );
            case HeroCharacter.VinFletcher:
                return new Character(
                    "VIN FLETCHER",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.Punch),
                    15,
                    GearCreator.CreateGearItem(GearItemEnum.VinsBow)
                );
            case HeroCharacter.Mylara:
                return new Character(
                    "MYLARA",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.CannonShot),
                    15
                );
            case HeroCharacter.Skorin:
                return new Character(
                    "SKORIN",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.CannonShot),
                    15
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
                    5
                );
            case MonsterCharacter.SkeletonWithDagger:
                return new Character(
                    "SKELETON",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.BoneCrunch),
                    5,
                    GearCreator.CreateGearItem(GearItemEnum.Dagger)
                );
            case MonsterCharacter.StoneAmarok:
                return new Character(
                    "STONE AMAROK",
                    actionChooser,
                    AttackCreator.CreateAttack(AttackEnum.Bite),
                    4,
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
                    15
                );
        }

        throw new NotImplementedException("Unknown Hero enountered.");
    }

    private static string GetCharacterName(HeroCharacter character)
    {
        return ConsoleHelpers.GetValidConsoleStringInput($"What is {character}'s name?");
    }
}
