using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle;

public class Battle
{
    public Party HeroesParty { get; private set; }
    public Party MonstersParty { get; private set; }

    public Battle(Party heroes, Party monsters)
    {
        HeroesParty = heroes;
        MonstersParty = monsters;
    }

    private void SubscribeToDeaths()
    {
        foreach (Character character in HeroesParty.Members)
            character.CharacterDied += OnCharacterDied;

        foreach (Character character in MonstersParty.Members)
            character.CharacterDied += OnCharacterDied;
    }

    public void Cleanup()
    {
        foreach (Character character in HeroesParty.Members)
            character.CharacterDied -= OnCharacterDied;

        foreach (Character character in MonstersParty.Members)
            character.CharacterDied -= OnCharacterDied;
    }

    public bool RunBattle()
    {
        bool gameEnded = false;
        SubscribeToDeaths();

        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Battle,
            "-----New battle commences!-----"
        );

        while (!gameEnded)
        {
            gameEnded = RunRound();
        }

        if (HeroesParty.Members.Count > 0)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Victory,
                $"{HeroesParty.Type} have won the battle!"
            );

            Cleanup();
            return true;
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

            Cleanup();
            return false;
        }
    }

    public void OnCharacterDied(Character character)
    {
        character.CharacterDied -= OnCharacterDied;
        Party characterParty = GetPartyFor(character);
        characterParty.Members.Remove(character);
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Attack,
            $"{character.Name} has been defeated!"
        );
    }

    private bool RunRound()
    {
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Time,
            $"---------------- {DateTime.UtcNow.ToString()} ----------------"
        );
        if (TakeTurnForParty(HeroesParty))
            return true;
        if (TakeTurnForParty(MonstersParty))
            return true;
        // Thread.Sleep(500);
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
