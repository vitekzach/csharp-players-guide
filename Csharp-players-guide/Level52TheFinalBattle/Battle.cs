using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;

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
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Info,
            StringHelpers.GetTableHeader(" Battle ", InfoBoxWidth)
        );

        string heroesHeader = StringHelpers.GetTableRow(
            $"{HeroesParty.Type}",
            "(HP)",
            TableStringHalfAlignment.Left,
            InfoBoxWidth,
            true
        );
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, heroesHeader);

        foreach (Character partyCharacter in HeroesParty.Members)
        {
            MessageType messageType;

            if (partyCharacter == character)
                messageType = MessageType.Victory;
            else
                messageType = MessageType.Info;

            string heroRow = StringHelpers.GetTableRow(
                $"  {partyCharacter.Name}",
                $"({partyCharacter.Hp}/{partyCharacter.HpInitial})",
                TableStringHalfAlignment.Left,
                InfoBoxWidth,
                false
            );
            ConsoleHelpers.WriteLineWithColoredConsole(messageType, heroRow);
        }

        string inventoryHeader = StringHelpers.GetTableRow(
            "Heroes inventory",
            "",
            TableStringHalfAlignment.Left,
            InfoBoxWidth,
            true
        );
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, inventoryHeader);

        foreach (ConsumableItem inventoryItem in HeroesParty.Inventory)
        {
            string inventoryRow = StringHelpers.GetTableRow(
                $"  {inventoryItem}",
                $"",
                TableStringHalfAlignment.Left,
                InfoBoxWidth,
                false
            );
            ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, inventoryRow);
        }

        string middleRow = StringHelpers.GetTableDivider(" VS ", InfoBoxWidth);
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, middleRow);

        string monsterHeader = StringHelpers.GetTableRow(
            $"{MonstersParty.Type}",
            "(HP)",
            TableStringHalfAlignment.Right,
            InfoBoxWidth,
            true
        );
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, monsterHeader);

        foreach (Character partyCharacter in MonstersParty.Members)
        {
            MessageType messageType;

            if (partyCharacter == character)
                messageType = MessageType.Victory;
            else
                messageType = MessageType.Info;

            string monsterRow = StringHelpers.GetTableRow(
                $"  {partyCharacter.Name}",
                $"({partyCharacter.Hp}/{partyCharacter.HpInitial})",
                TableStringHalfAlignment.Right,
                InfoBoxWidth,
                false
            );
            ConsoleHelpers.WriteLineWithColoredConsole(messageType, monsterRow);
        }

        inventoryHeader = StringHelpers.GetTableRow(
            "Monster inventory",
            "",
            TableStringHalfAlignment.Right,
            InfoBoxWidth,
            true
        );
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, inventoryHeader);

        foreach (ConsumableItem inventoryItem in MonstersParty.Inventory)
        {
            string inventoryRow = StringHelpers.GetTableRow(
                $"  {inventoryItem}",
                $"",
                TableStringHalfAlignment.Right,
                InfoBoxWidth,
                false
            );
            ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, inventoryRow);
        }

        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Info,
            StringHelpers.GetTableBottom(InfoBoxWidth)
        );
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
