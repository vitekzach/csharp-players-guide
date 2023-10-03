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

    public void OnCharacterDied(Character deadCharacter, Character attacker)
    {
        deadCharacter.CharacterDied -= OnCharacterDied;
        Party characterParty = GetPartyFor(deadCharacter);
        Party enemyParty = GetEnemyPartyFor(deadCharacter);
        enemyParty.GainedXP += deadCharacter.XP;
        characterParty.Members.Remove(deadCharacter);
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Attack,
            $"{deadCharacter.Name} has been defeated! {enemyParty.Type} gained {deadCharacter.XP} XP."
        );
        if (deadCharacter.EquippedGear.Any())
        {
            var gearNames = deadCharacter.EquippedGear.Select(x => x.Name);
            string gearNamesJoined = string.Join(',', gearNames);
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Info,
                $"{deadCharacter.Name}'s gear ({gearNamesJoined}) has been acquired."
            );
            enemyParty.Inventory.AddRange(deadCharacter.EquippedGear);
            deadCharacter.EquippedGear.Clear();
        }
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
            $"{HeroesParty}",
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
                $"  {partyCharacter}",
                $"({partyCharacter.Hp}/{partyCharacter.HpMax})",
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

        foreach (InventoryItem inventoryItem in HeroesParty.Inventory)
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
            $"{MonstersParty}",
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
                $"  {partyCharacter}",
                $"({partyCharacter.Hp}/{partyCharacter.HpMax})",
                TableStringHalfAlignment.Right,
                InfoBoxWidth,
                false
            );
            ConsoleHelpers.WriteLineWithColoredConsole(messageType, monsterRow);
        }

        inventoryHeader = StringHelpers.GetTableRow(
            "Monsters inventory",
            "",
            TableStringHalfAlignment.Right,
            InfoBoxWidth,
            true
        );
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, inventoryHeader);

        foreach (InventoryItem inventoryItem in MonstersParty.Inventory)
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
        party.Inventory.Sort(new InventoryItemComparer());
        foreach (Character character in party.Members)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Info,
                $"It is {character.Name}'s turn..."
            );
            PrintCharacterTurnInfo(character);

            character.TakeTurn(this);
            ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Info, "");
            if (CheckForBattleEnd(character, party))
                return true;
        }
        return false;
    }


    private bool CheckForBattleEnd(Character character, Party party)
    {
        Party enemyParty = GetEnemyPartyFor(character);
        if (enemyParty.Members.Count == 0)
        {
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Attack,
                $"{enemyParty.Type} have all died!"
            );

            party.Inventory.AddRange(enemyParty.Inventory);
            enemyParty.Inventory.Clear();
            ConsoleHelpers.WriteLineWithColoredConsole(
                MessageType.Info,
                $"{enemyParty.Type}'s whole inventory has been transfered to {party.Type}."
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
