using System.Diagnostics.Tracing;
using System.Security.AccessControl;
using System.Security.Cryptography;
using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle.Characters;

public class Character
{
    public event Action<Character>? CharacterDied;
    public string Name { get; init; }
    public List<CharacterMove> Moves { get; private set; }
    private IChooseActionInterface ActionChooser { get; init; }

    public Attack Attack { get; init; }

    public GearItem? EquippedGear { get; protected set; }

    public int HpInitial { get; init; }
    public int Hp { get; set; }

    public Character(
        string name,
        IChooseActionInterface actionChooser,
        Attack attack,
        int hpInitial,
        GearItem? startingGearItem
    )
    {
        Name = name;
        Moves = new List<CharacterMove>() { CharacterMove.Nothing, CharacterMove.Attack };
        ActionChooser = actionChooser;
        Attack = attack;
        HpInitial = hpInitial;
        Hp = hpInitial;
        EquippedGear = startingGearItem;
        if (startingGearItem != null)
            Moves.Add(CharacterMove.GearAttack);
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
            AttackAction attackAction = new AttackAction(attack, chosenEnemy);
            attackAction.Run();
        }
        else
            Console.WriteLine($"{Name} did {move}.");
    }

    public void TakeDamage(int damage)
    {
        Hp = Math.Clamp(Hp - damage, 0, HpInitial);
        if (Hp == 0)
        {
            CharacterDied?.Invoke(this);
        }
    }

    public void DoNothing() { }

    public void TakeHealing(int healing)
    {
        Hp = Math.Clamp(Hp + healing, 0, HpInitial);
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
