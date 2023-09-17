using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Helpers;
using System.Security.Cryptography;

namespace Level52TheFinalBattle.Characters;

public class Character
{
    public event Action<Character>? CharacterDied;
    public string Name { get; init; }
    public List<CharacterMove> Moves { get; init; }
    private IChooseActionInterface ActionChooser { get; init; }

    public Attack Attack { get; init; }

    public int HpInitial { get; init; }
    public int Hp { get; set; }

    public Character(
        string name,
        IChooseActionInterface actionChooser,
        Attack attack,
        int hpInitial
    )
    {
        Name = name;
        Moves = new List<CharacterMove>() { CharacterMove.Nothing, CharacterMove.Attack };
        ActionChooser = actionChooser;
        Attack = attack;
        HpInitial = hpInitial;
        Hp = hpInitial;
    }

    public virtual void TakeTurn(Battle battle)
    {
        CharacterMove move = ActionChooser.ChooseAction(this);
        if (move == CharacterMove.Attack)
        {
            Character chosenEnemy = ActionChooser.ChooseEnemyTarget(this, battle);

            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Normal,
                $"{Name} used {Attack.Name} against {chosenEnemy.Name}."
            );
            AttackAction attackAction = new AttackAction(Attack, chosenEnemy);
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

    public override string ToString()
    {
        return Name;
    }
}
