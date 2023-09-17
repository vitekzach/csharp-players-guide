using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle;

public class Battle
{
    public Party HeroesParty { get; private set; }
    public Party MonstersParty { get; private set; }

    private int InfoBoxWidth { get; } = 100;

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
            MessageType.Info,
            StringHelpers.CenterString($" {DateTime.UtcNow.ToString()} ", '═', InfoBoxWidth)
        );
        if (TakeTurnForParty(HeroesParty))
            return true;
        if (TakeTurnForParty(MonstersParty))
            return true;
        // Thread.Sleep(500);
        return false;
    }

    private void PrintCharacterTurnInfo(Character character)
    {
        string topRow = StringHelpers.CenterString(" BATTLE ", '═', InfoBoxWidth);
        topRow = $"╔{topRow}╗";

        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, topRow);

        string heroesHeader = StringHelpers.LeftAndRightJustify(
            $"{HeroesParty.Type}",
            "(HP)",
            '─',
            InfoBoxWidth / 2
        );
        heroesHeader = StringHelpers.LeftJustify(heroesHeader, ' ', InfoBoxWidth);
        heroesHeader = $"║{heroesHeader}║";
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, heroesHeader);

        foreach (Character partyCharacter in HeroesParty.Members)
        {
            MessageType messageType;

            if (partyCharacter == character)
                messageType = MessageType.Victory;
            else
                messageType = MessageType.Info;

            string heroRow = StringHelpers.LeftAndRightJustify(
                $"{partyCharacter.Name}",
                $"({partyCharacter.Hp}/{partyCharacter.HpInitial})",
                ' ',
                InfoBoxWidth / 2
            );
            heroRow = StringHelpers.LeftJustify(heroRow, ' ', InfoBoxWidth);
            heroRow = $"║{heroRow}║";

            ConsoleHelpers.WriteLineWithColoredConsole(messageType, heroRow);
        }

        string middleRow = StringHelpers.CenterString(" VS ", '━', InfoBoxWidth);
        middleRow = $"╟{middleRow}╢";

        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, middleRow);

        string monstersHeader = StringHelpers.LeftAndRightJustify(
            $"{MonstersParty.Type}",
            "(HP)",
            '─',
            InfoBoxWidth / 2
        );
        monstersHeader = StringHelpers.RightJustify(monstersHeader, ' ', InfoBoxWidth);
        monstersHeader = $"║{monstersHeader}║";
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, monstersHeader);

        foreach (Character partyCharacter in MonstersParty.Members)
        {
            MessageType messageType;

            if (partyCharacter == character)
                messageType = MessageType.Victory;
            else
                messageType = MessageType.Info;

            string monsterRow = StringHelpers.LeftAndRightJustify(
                $"{partyCharacter.Name}",
                $"({partyCharacter.Hp}/{partyCharacter.HpInitial})",
                ' ',
                InfoBoxWidth / 2
            );
            monsterRow = StringHelpers.RightJustify(monsterRow, ' ', InfoBoxWidth);
            monsterRow = $"║{monsterRow}║";

            ConsoleHelpers.WriteLineWithColoredConsole(messageType, monsterRow);
        }

        string bottomRow = StringHelpers.CenterString("", '═', InfoBoxWidth);
        bottomRow = $"╚{bottomRow}╝";

        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, bottomRow);
    }

    private bool TakeTurnForParty(Party party)
    {
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Team,
            $"TEAM TURN: It is {party.Type}' turn."
        );
        foreach (Character character in party.Members)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Info,
                $"It is {character.Name}'s turn..."
            );
            PrintCharacterTurnInfo(character);

            character.TakeTurn(this);
            ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, "");
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
