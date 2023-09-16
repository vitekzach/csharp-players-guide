using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle;

public class Battle
{
    public Party HeroesParty { get; private set; }
    public Party MonstersParty { get; private set; }

    public Battle()
    {
        HeroesParty = new Party(
            new List<Character>() { new TheTrueProgrammer() },
            PartyType.Heroes
        );
        MonstersParty = new Party(
            new List<Character>() { new SkeletonCharacter() },
            PartyType.Monsters
        );

        SubscribeToDeaths();
    }

    public Battle(Party heroes, Party monsters)
    {
        HeroesParty = heroes;
        MonstersParty = monsters;

        SubscribeToDeaths();
    }

    private void SubscribeToDeaths()
    {
        foreach (Character character in HeroesParty.Members)
            character.CharacterDied += OnCharacterDied;

        foreach (Character character in MonstersParty.Members)
            character.CharacterDied += OnCharacterDied;
    }

    public void RunBattle()
    {
        bool gameEnded = false;

        while (!gameEnded)
        {
            gameEnded = RunRound();
        }

        if (HeroesParty.Members.Count > 0)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Victory,
                $"{HeroesParty.Type} have won!"
            );
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Victory,
                $"The Uncoded One was defeated."
            );
        }
        else
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Loss,
                $"{HeroesParty.Type} have lost!"
            );
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Loss,
                $"The Uncoded One's forces have prevailed."
            );
        }
    }

    public void OnCharacterDied(Character character)
    {
        Party characterParty = GetPartyFor(character);
        characterParty.Members.Remove(character);
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Attack,
            $"{character.Name} has been defeated!"
        );
        character.CharacterDied -= OnCharacterDied;
    }

    private bool RunRound()
    {
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Time, DateTime.UtcNow.ToString());
        if (TakeTurnForParty(HeroesParty))
            return true;
        if (TakeTurnForParty(MonstersParty))
            return true;
        Thread.Sleep(500);
        return false;
    }

    private bool TakeTurnForParty(Party party)
    {
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Team,
            $"TEAM TURN: It is {party.Type}' turn."
        );
        foreach (Character character in party.Members)
        {
            Console.WriteLine($"It is {character.Name}'s turn...");
            character.TakeTurn(this);
            Console.WriteLine();
            if (CheckForGameEnd(character))
                return true;
        }
        return false;
    }

    private bool CheckForGameEnd(Character character)
    {
        Party enemyParty = GetEnemyPartyFor(character);
        if (enemyParty.Members.Count == 0)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Attack,
                $"{enemyParty.Type} have all died!"
            );
            return true;
        }
        return false;
    }

    public bool CharacterHero(Character character)
    {
        if (HeroesParty.Members.Contains(character))
            return true;
        return false;
    }

    public Party GetPartyFor(Character character)
    {
        if (CharacterHero(character))
            return HeroesParty;
        else
            return MonstersParty;
    }

    public Party GetEnemyPartyFor(Character character)
    {
        if (CharacterHero(character))
            return MonstersParty;
        else
            return HeroesParty;
    }
}
