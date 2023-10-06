using System.Globalization;
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

    private RoundTablePrinter _tablePrinter;

    public Battle(Party heroes, Party monsters)
    {
        HeroesParty = heroes;
        MonstersParty = monsters;
        _tablePrinter = new RoundTablePrinter();
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
            _tablePrinter.PrintInformationTable(this, character);
            // PrintCharacterTurnInfo(character);

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

internal class RoundTablePrinter
{
    const ConsoleColor _backgroundColor = ConsoleColor.Black;
    const ConsoleColor _heroColor = ConsoleColor.White;
    private readonly ConsoleColor _tableBorderColor = ConsoleColor.Blue;
    private readonly char _healthSymbol = '▉';

    private readonly int _tableWidth = 100;
    private readonly int _ListMemberPadding = 2;

    private readonly string _verticalOutsideBorder = "║";
    private readonly char _horizontalOutsideBorder = '═';
    private readonly string _topLeftCorner = "╔";
    private readonly string _topRightCorner = "╗";
    private readonly string _bottomLeftCorner = "╚";
    private readonly string _bottomRightCorner = "╝";
    private readonly string _dividerLeft = "╟";
    private readonly char _divider = '━';
    private readonly string _dividerRight = "╢";

    // private readonly string _verticalOutsideBorder = "║";

    internal void PrintInformationTable(Battle battle, Character character)
    {
        PrintTopRow();
        PrintParty(battle.HeroesParty, character);
        PrintMiddleRow();
        PrintParty(battle.MonstersParty, character);
        PrintBottomRow();
    }

    private void PrintParty(Party party, Character character)
    {
        PrintPartyHeader(party);
        foreach (Character characterToPrint in party.Members)
        {
            var charName = GetCharacterLine(characterToPrint, party.Type, character);
            PrintLine(charName);
        }
        PrintPartyInventoryHeader(party);
        foreach (InventoryItem inventoryItem in party.Inventory)
        {
            var inventoryLine = GetInventoryLine(inventoryItem, party.Type);
            PrintLine(inventoryLine);
        }
    }

    private void PrintPartyHeader(Party party)
    {
        var backgroundColor = ConsoleColor.Black;
        var header = new List<ConsoleText>()
        {
            new ConsoleText(_verticalOutsideBorder, _tableBorderColor, ConsoleColor.Black),
            new ConsoleText(
                $"{party.Type.ToString()} ({party.GainedXP} XP)",
                _tableBorderColor,
                _backgroundColor
            ),
            new ConsoleText("(HP)", _tableBorderColor, _backgroundColor),
        };

        int insertPaddingIndex;
        if (party.Type == PartyType.Heroes)
            insertPaddingIndex = header.Count + 1;
        else
            insertPaddingIndex = 1;

        PadTextList(
            header,
            header.Count - 1,
            _tableWidth / 2,
            '─',
            foregroundColor: _tableBorderColor
        );
        header.Insert(
            header.Count,
            new ConsoleText(_verticalOutsideBorder, _tableBorderColor, ConsoleColor.Black)
        );
        PadTextList(
            header,
            insertPaddingIndex,
            _tableWidth,
            ' ',
            foregroundColor: _tableBorderColor
        );
        PrintLine(header);
    }

    private void PrintPartyInventoryHeader(Party party)
    {
        var header = new List<ConsoleText>()
        {
            new ConsoleText(_verticalOutsideBorder, _tableBorderColor, ConsoleColor.Black),
            new ConsoleText(
                $"{party.Type.ToString()} inventory",
                _tableBorderColor,
                _backgroundColor
            ),
        };

        int insertPaddingIndex;
        if (party.Type == PartyType.Heroes)
            insertPaddingIndex = header.Count + 1;
        else
            insertPaddingIndex = 1;

        PadTextList(header, header.Count, _tableWidth / 2, '─', foregroundColor: _tableBorderColor);
        header.Insert(
            header.Count,
            new ConsoleText(_verticalOutsideBorder, _tableBorderColor, ConsoleColor.Black)
        );
        PadTextList(
            header,
            insertPaddingIndex,
            _tableWidth,
            ' ',
            foregroundColor: _tableBorderColor
        );
        PrintLine(header);
    }

    private void PrintLine(List<ConsoleText> texts)
    {
        foreach (ConsoleText text in texts)
        {
            WriteConsoleText(text);
        }
        Console.WriteLine();
    }

    private List<ConsoleText> GetCharacterLine(
        Character character,
        PartyType partyType,
        Character charactersTurn
    )
    {
        ConsoleColor backgroundColor = _backgroundColor;
        ConsoleColor foregroundColor = _heroColor;

        if (character == charactersTurn)
        {
            backgroundColor = ConsoleColor.DarkGreen;
            foregroundColor = ConsoleColor.Black;
        }

        var characterLine = new List<ConsoleText>()
        {
            new ConsoleText(_verticalOutsideBorder, _tableBorderColor, ConsoleColor.Black)
        };
        List<ConsoleText> characterName = GetCharacterName(
            character,
            backgroundColor: backgroundColor,
            foregroundColor: foregroundColor
        );

        List<ConsoleText> characterHealth = GetCharacterHealth(
            character,
            partyType,
            backgroundColor: backgroundColor,
            foregroundColor: foregroundColor
        );
        int insertPaddingIndex;

        if (partyType == PartyType.Heroes)
        {
            characterLine.AddRange(characterName);
            characterLine.AddRange(characterHealth);
            insertPaddingIndex = characterLine.Count - 3;
        }
        else //if (partyType == PartyType.Monsters)
        {
            characterLine.AddRange(characterHealth);
            characterLine.AddRange(characterName);
            insertPaddingIndex = 4;
        }

        characterLine.Add(
            new ConsoleText(_verticalOutsideBorder, _tableBorderColor, ConsoleColor.Black)
        );

        PadTextList(
            characterLine,
            insertPaddingIndex,
            _tableWidth,
            backgroundColor: backgroundColor,
            foregroundColor: foregroundColor
        );
        return characterLine;
    }

    private List<ConsoleText> GetInventoryLine(InventoryItem item, PartyType partyType)
    {
        int insertPaddingIndex;
        var inventoryLine = new List<ConsoleText>()
        {
            new ConsoleText(_verticalOutsideBorder, _tableBorderColor, ConsoleColor.Black),
            new ConsoleText(
                $"{new string(' ', _ListMemberPadding)}{item.ToString()}",
                _heroColor,
                _backgroundColor
            ),
            new ConsoleText(_verticalOutsideBorder, _tableBorderColor, ConsoleColor.Black),
        };

        if (partyType == PartyType.Heroes)
        {
            insertPaddingIndex = inventoryLine.Count - 1;
        }
        else //if (partyType == PartyType.Monsters)
        {
            PadTextList(inventoryLine, 2, _tableWidth / 2, ' ');
            insertPaddingIndex = 1;
        }

        PadTextList(inventoryLine, insertPaddingIndex, _tableWidth);
        return inventoryLine;
    }

    private List<ConsoleText> GetCharacterName(
        Character character,
        ConsoleColor backgroundColor = _backgroundColor,
        ConsoleColor foregroundColor = _backgroundColor
    )
    {
        var heroName = new List<ConsoleText>();
        heroName.Add(
            new ConsoleText(new string(' ', _ListMemberPadding), foregroundColor, backgroundColor)
        );
        string warningSymbols = "[!]";
        if (character.Hp <= character.HpMax / 4)
            heroName.Add(new ConsoleText(warningSymbols, ConsoleColor.Yellow, backgroundColor));
        if (character.Hp <= character.HpMax / 10)
            heroName.Add(new ConsoleText(warningSymbols, ConsoleColor.Red, backgroundColor));
        heroName.Add(new ConsoleText(character.ToString(), foregroundColor, backgroundColor));
        heroName.Add(
            new ConsoleText($"({character.Hp}/{character.HpMax})", foregroundColor, backgroundColor)
        );
        // PrintLine(heroName);
        PadTextList(
            heroName,
            heroName.Count - 1,
            (_tableWidth - 1) / 2,
            foregroundColor: foregroundColor,
            backgroundColor: backgroundColor
        );
        // PrintLine(heroName);
        return heroName;
    }

    private List<ConsoleText> GetCharacterHealth(
        Character character,
        PartyType partyType,
        ConsoleColor backgroundColor = _backgroundColor,
        ConsoleColor foregroundColor = _backgroundColor
    )
    {
        var heroHealth = new List<ConsoleText>();
        string lostHp = new string(_healthSymbol, character.HpMax - character.Hp);
        string currentHp = new string(_healthSymbol, character.Hp);
        if (partyType == PartyType.Heroes)
        {
            heroHealth.Add(new ConsoleText(currentHp, ConsoleColor.Green, backgroundColor));
            heroHealth.Add(new ConsoleText(lostHp, ConsoleColor.Red, backgroundColor));
        }
        else
        {
            heroHealth.Add(new ConsoleText(lostHp, ConsoleColor.Red, backgroundColor));
            heroHealth.Add(new ConsoleText(currentHp, ConsoleColor.Green, backgroundColor));
        }
        int insertPaddingIndex;
        if (partyType == PartyType.Heroes)
            insertPaddingIndex = heroHealth.Count;
        else
            insertPaddingIndex = 0;
        PadTextList(
            heroHealth,
            insertPaddingIndex,
            (_tableWidth - 10) / 2,
            backgroundColor: backgroundColor,
            foregroundColor: foregroundColor
        );
        return heroHealth;
    }

    private void WriteConsoleText(ConsoleText text)
    {
        Console.BackgroundColor = text.BackGroundColor;
        Console.ForegroundColor = text.ForegroundColor;
        Console.Write(text.Text);
        Console.ResetColor();
    }

    private void PadTextList(
        List<ConsoleText> texts,
        int insertPaddingIndex,
        int desiredWidth,
        char paddingChar = ' ',
        ConsoleColor backgroundColor = _backgroundColor,
        ConsoleColor foregroundColor = _backgroundColor
    )
    {
        var padding = new ConsoleText(
            new string(paddingChar, Math.Max(0, desiredWidth - GetLengthOfConsoleTexts(texts))),
            foregroundColor,
            backgroundColor
        );

        texts.Insert(insertPaddingIndex, padding);
    }

    private int GetLengthOfConsoleTexts(List<ConsoleText> texts)
    {
        return texts.Sum(x => x.Text.Length);
    }

    private void PrintTopRow()
    {
        string title = " Battle ";
        int paddingLeft = (_tableWidth - title.Length - 2) / 2;
        int paddingRight = (paddingLeft % 2 != 0) ? paddingLeft : paddingLeft - 1;
        var tableTitle = new List<ConsoleText>()
        {
            new ConsoleText(_topLeftCorner, _tableBorderColor, ConsoleColor.Black),
            new ConsoleText(
                new string(_horizontalOutsideBorder, paddingLeft),
                _tableBorderColor,
                ConsoleColor.Black
            ),
            new ConsoleText(title, _tableBorderColor, ConsoleColor.Black),
            new ConsoleText(
                new string(_horizontalOutsideBorder, paddingRight),
                _tableBorderColor,
                ConsoleColor.Black
            ),
            new ConsoleText(_topRightCorner, _tableBorderColor, ConsoleColor.Black),
        };
        PrintLine(tableTitle);
    }

    private void PrintMiddleRow()
    {
        string title = " VS ";
        int paddingLeft = (_tableWidth - title.Length - 2) / 2;
        int paddingRight = (paddingLeft % 2 != 0) ? paddingLeft : paddingLeft - 1;
        var tableTitle = new List<ConsoleText>()
        {
            new ConsoleText(_dividerLeft, _tableBorderColor, ConsoleColor.Black),
            new ConsoleText(
                new string(_divider, paddingLeft),
                _tableBorderColor,
                ConsoleColor.Black
            ),
            new ConsoleText(title, _tableBorderColor, ConsoleColor.Black),
            new ConsoleText(
                new string(_divider, paddingRight),
                _tableBorderColor,
                ConsoleColor.Black
            ),
            new ConsoleText(_dividerRight, _tableBorderColor, ConsoleColor.Black),
        };
        PrintLine(tableTitle);
    }

    private void PrintBottomRow()
    {
        var tableTitle = new List<ConsoleText>()
        {
            new ConsoleText(_bottomLeftCorner, _tableBorderColor, ConsoleColor.Black),
            new ConsoleText(
                new string(_horizontalOutsideBorder, _tableWidth - 2),
                _tableBorderColor,
                ConsoleColor.Black
            ),
            new ConsoleText(_bottomRightCorner, _tableBorderColor, ConsoleColor.Black),
        };
        PrintLine(tableTitle);
    }
}
