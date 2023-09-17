using Level52TheFinalBattle.ActionChoosers;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle;

public class Game
{
    private List<Battle> Battles { get; set; }

    public Game()
    {
        GameMode chosenGameMode = GetGameMode();

        // Party heroesParty;
        // Party monstersParty1;
        // Party monstersParty2;
        // Party uncodedOnesParty;

        // switch (chosenGameMode)
        // {
        //     case GameMode.AIvsAI:
        //         heroesParty = new Party(
        //             new List<Character>() { new TheTrueProgrammer(new AIAction()) },
        //             PartyType.Heroes
        //         );
        //         monstersParty1 = new Party(
        //             new List<Character>() { new SkeletonCharacter(new AIAction()) },
        //             PartyType.Monsters
        //         );
        //         monstersParty2 = new Party(
        //             new List<Character>()
        //             {
        //                 new SkeletonCharacter(new AIAction()),
        //                 new SkeletonCharacter(new AIAction())
        //             },
        //             PartyType.Monsters
        //         );
        //         uncodedOnesParty = new Party(
        //             new List<Character>() { new TheUncodedOne(new AIAction()) },
        //             PartyType.Monsters
        //         );
        //         break;
        //     case GameMode.HumanVsAI:
        //         heroesParty = new Party(
        //             new List<Character>() { new TheTrueProgrammer(new ConsoleAction()) },
        //             PartyType.Heroes
        //         );
        //         monstersParty1 = new Party(
        //             new List<Character>() { new SkeletonCharacter(new AIAction()) },
        //             PartyType.Monsters
        //         );
        //         monstersParty2 = new Party(
        //             new List<Character>()
        //             {
        //                 new SkeletonCharacter(new AIAction()),
        //                 new SkeletonCharacter(new AIAction())
        //             },
        //             PartyType.Monsters
        //         );
        //         uncodedOnesParty = new Party(
        //             new List<Character>() { new TheUncodedOne(new AIAction()) },
        //             PartyType.Monsters
        //         );
        //         break;
        //     case GameMode.HumanvsHuman:
        //         heroesParty = new Party(
        //             new List<Character>() { new TheTrueProgrammer(new ConsoleAction()) },
        //             PartyType.Heroes
        //         );
        //         monstersParty1 = new Party(
        //             new List<Character>() { new SkeletonCharacter(new ConsoleAction()) },
        //             PartyType.Monsters
        //         );
        //         monstersParty2 = new Party(
        //             new List<Character>()
        //             {
        //                 new SkeletonCharacter(new ConsoleAction()),
        //                 new SkeletonCharacter(new ConsoleAction())
        //             },
        //             PartyType.Monsters
        //         );
        //         uncodedOnesParty = new Party(
        //             new List<Character>() { new TheUncodedOne(new ConsoleAction()) },
        //             PartyType.Monsters
        //         );
        //         break;
        // }

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
            new List<Character>() { new TheTrueProgrammer(heroActionChooser) },
            PartyType.Heroes
        );
        Party monstersParty1 = new Party(
            new List<Character>() { new SkeletonCharacter(monsterActionChooser) },
            PartyType.Monsters
        );
        Party monstersParty2 = new Party(
            new List<Character>()
            {
                new SkeletonCharacter(monsterActionChooser),
                new SkeletonCharacter(monsterActionChooser)
            },
            PartyType.Monsters
        );
        Party uncodedOnesParty = new Party(
            new List<Character>() { new TheUncodedOne(monsterActionChooser) },
            PartyType.Monsters
        );

        Battles = new List<Battle>()
        {
            new Battle(heroesParty, monstersParty1),
            new Battle(heroesParty, monstersParty2),
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
