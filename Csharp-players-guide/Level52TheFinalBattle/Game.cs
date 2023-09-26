using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Attacks;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle;

public class Game
{
    private List<Battle> Battles { get; set; }

    public Game()
    {
        GameMode chosenGameMode = GetGameMode();

        IChooseActionInterface heroActionChooser = new AIAction();
        IChooseActionInterface monsterActionChooser = new AIAction();

        switch (chosenGameMode)
        {
            case GameMode.AIvsAI:
                heroActionChooser = new AIAction();
                monsterActionChooser = new AIAction();
                break;
            case GameMode.HumanVsAI:
                heroActionChooser = new ConsoleAction();
                monsterActionChooser = new AIAction();
                break;
            case GameMode.HumanvsHuman:
                heroActionChooser = new ConsoleAction();
                monsterActionChooser = new ConsoleAction();
                break;
        }

        Party heroesParty = new Party(
            new List<Character>()
            {
                new TheTrueProgrammer(
                    heroActionChooser,
                    new GearItem("Sword", new SlashAttack(), -1)
                ),
                new VinFletcher(
                    heroActionChooser,
                    new GearItem("Vin's bow", new QuickShotAttack(), -1)
                )
            },
            PartyType.Heroes,
            new List<InventoryItem>()
            {
                new HealthPotionItem(),
                new HealthPotionItem(),
                new HealthPotionItem(),
                // new GearItem("Sword2", new SlashAttack())
            }
        );
        Party monstersParty1 = new Party(
            new List<Character>()
            {
                new SkeletonCharacter(
                    monsterActionChooser,
                    new GearItem("Dagger", new StabAttack(), -1)
                )
            },
            PartyType.Monsters,
            new List<InventoryItem>() { new HealthPotionItem() }
        );
        Party monstersParty2 = new Party(
            new List<Character>()
            {
                new SkeletonCharacter(monsterActionChooser),
                new SkeletonCharacter(monsterActionChooser)
            },
            PartyType.Monsters,
            new List<InventoryItem>()
            {
                new HealthPotionItem(),
                new GearItem("Dagger", new StabAttack()),
                new GearItem("Dagger", new StabAttack())
            }
        );
        Party monstersParty3 = new Party(
            new List<Character>()
            {
                new StoneAmarok(monsterActionChooser, null, new AttackModifier("STONE ARMOR", -1)),
                new StoneAmarok(monsterActionChooser, null, new AttackModifier("STONE ARMOR", -1))
            },
            PartyType.Monsters,
            new List<InventoryItem>() { }
        );
        Party uncodedOnesParty = new Party(
            new List<Character>() { new TheUncodedOne(monsterActionChooser) },
            PartyType.Monsters,
            new List<InventoryItem>() { new HealthPotionItem() }
        );

        Battles = new List<Battle>()
        {
            new Battle(heroesParty, monstersParty1),
            new Battle(heroesParty, monstersParty2),
            new Battle(heroesParty, monstersParty3),
            new Battle(heroesParty, uncodedOnesParty)
        };
    }

    private GameMode GetGameMode()
    {
        List<GameMode> gameModes = Enum.GetValues(typeof(GameMode)).Cast<GameMode>().ToList();
        int gameModeChoice = ConsoleHelpers.GetValidConsoleIntegerInputBasedOnListIndex<GameMode>(
            "Choose gamemode: ",
            gameModes,
            PrintAvailableGameModes
        );
        return gameModes[gameModeChoice];
    }

    private void PrintAvailableGameModes(List<GameMode> gameModes)
    {
        ConsoleHelpers.WriteLineWithColoredConsole(
            MessageType.Choice,
            "Choose from the following modes:"
        );
        ConsoleHelpers.PrintChoicesFromList<GameMode>(gameModes);
    }

    public void Run()
    {
        foreach (Battle battle in Battles)
        {
            if (!battle.RunBattle())
                break;
        }
    }
}
