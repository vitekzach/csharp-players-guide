using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Helpers;
using System.Security.Cryptography;

namespace Level52TheFinalBattle.Characters;

public class Character
{
    public string Name { get; init; }
    public List<CharacterMoves> Moves { get; init; }
    private IChooseActionInterface ActionChooser { get; init; }

    public Attack Attack { get; init; }

    private Random RandomNumberGenerator { get; } = new Random();

    public Character(string name, IChooseActionInterface actionChooser, Attack attack)
    {
        Name = name;
        Moves = new List<CharacterMoves>() { CharacterMoves.Nothing, CharacterMoves.Attack };
        ActionChooser = actionChooser;
        Attack = attack;
    }

    public virtual void TakeTurn(Battle battle)
    {
        CharacterMoves move = ActionChooser.ChooseAction(this);
        if (move == CharacterMoves.Attack)
        {
            Party enemyParty = battle.GetEnemyPartyFor(this);
            int enemyIndexChoice =
                ConsoleHelpers.GetValidConsoleIntegerInputBasedOnListIndex<Character>(
                    "Enemy chosen: ",
                    enemyParty.Members,
                    ConsoleHelpers.PrintChoicesFromList<Character>
                );

            Character chosenEnemy = enemyParty.Members[enemyIndexChoice];

            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Normal,
                $"{Name} used {Attack.Name} against {chosenEnemy.Name}."
            );
        }
        else
            Console.WriteLine($"{Name} did {move}.");
    }

    protected void AiTakeTurn(Battle battle)
    {
        CharacterMoves move = ActionChooser.ChooseAction(this);
        if (move == CharacterMoves.Attack)
        {
            Party enemyParty = battle.GetEnemyPartyFor(this);

            int enemyIndexChoice = PickRandomPartyMember(enemyParty);

            Character chosenEnemy = enemyParty.Members[enemyIndexChoice];

            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Normal,
                $"{Name} used {Attack.Name} against {chosenEnemy.Name}."
            );
        }
        else
            Console.WriteLine($"{Name} did {move}.");
    }

    private int PickRandomPartyMember(Party party)
    {
        return RandomNumberGenerator.Next(party.Members.Count);
    }

    public override string ToString()
    {
        return Name;
    }
}
