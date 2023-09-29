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

        ActionChooserEnum heroActionChooser;
        ActionChooserEnum monsterActionChooser;

        switch (chosenGameMode)
        {
            case GameMode.AIvsAI:
                heroActionChooser = ActionChooserEnum.AI;
                monsterActionChooser = ActionChooserEnum.AI;
                break;
            case GameMode.HumanVsAI:
                heroActionChooser = ActionChooserEnum.Human;
                monsterActionChooser = ActionChooserEnum.AI;
                break;
            case GameMode.HumanvsHuman:
                heroActionChooser = ActionChooserEnum.Human;
                monsterActionChooser = ActionChooserEnum.Human;
                break;
            default:
                throw new NotImplementedException("Unkown game mode encountered.");
        }

        Battles = InitDefaultGame(heroActionChooser, monsterActionChooser);
    }

    private List<Battle> InitDefaultGame(
        ActionChooserEnum heroActionChooser,
        ActionChooserEnum monsterActionChooser
    )
    {
        Party heroesParty = new Party(
            new List<Character>()
            {
                CharacterCreator.CreateHeroCharacter(
                    HeroCharacter.TheTrueProgrammer,
                    heroActionChooser
                ),
                CharacterCreator.CreateHeroCharacter(HeroCharacter.VinFletcher, heroActionChooser),
                CharacterCreator.CreateHeroCharacter(HeroCharacter.Mylara, heroActionChooser),
                CharacterCreator.CreateHeroCharacter(HeroCharacter.Skorin, heroActionChooser)
            },
            PartyType.Heroes,
            new List<InventoryItem>()
            {
                new HealthPotionItem(HealingItemEnum.BigPotion),
                new HealthPotionItem(HealingItemEnum.BigPotion),
                new HealthPotionItem(HealingItemEnum.SimulasSoup),
                // new GearItem("Sword2", new SlashAttack())
            }
        );
        Party monstersParty1 = new Party(
            new List<Character>()
            {
                CharacterCreator.CreateMonsterCharacter(
                    MonsterCharacter.SkeletonWithDagger,
                    monsterActionChooser
                )
            },
            PartyType.Monsters,
            new List<InventoryItem>() { new HealthPotionItem(HealingItemEnum.BigPotion) }
        );
        Party monstersParty2 = new Party(
            new List<Character>()
            {
                CharacterCreator.CreateMonsterCharacter(
                    MonsterCharacter.Skeleton,
                    monsterActionChooser
                ),
                CharacterCreator.CreateMonsterCharacter(
                    MonsterCharacter.Skeleton,
                    monsterActionChooser
                )
            },
            PartyType.Monsters,
            new List<InventoryItem>()
            {
                new HealthPotionItem(HealingItemEnum.BigPotion),
                GearCreator.CreateGearItem(GearItemEnum.Dagger),
                GearCreator.CreateGearItem(GearItemEnum.Dagger),
            }
        );
        Party monstersParty3 = new Party(
            new List<Character>()
            {
                CharacterCreator.CreateMonsterCharacter(
                    MonsterCharacter.StoneAmarok,
                    monsterActionChooser
                ),
                CharacterCreator.CreateMonsterCharacter(
                    MonsterCharacter.StoneAmarok,
                    monsterActionChooser
                ),
            },
            PartyType.Monsters,
            new List<InventoryItem>() { }
        );
        Party uncodedOnesParty = new Party(
            new List<Character>()
            {
                CharacterCreator.CreateMonsterCharacter(
                    MonsterCharacter.TheUncodedOne,
                    monsterActionChooser
                )
            },
            PartyType.Monsters,
            new List<InventoryItem>() { new HealthPotionItem(HealingItemEnum.BigPotion) }
        );

        return new List<Battle>()
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
