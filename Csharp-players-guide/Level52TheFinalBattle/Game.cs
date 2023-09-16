using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle;

public class Game
{
    private List<Battle> Battles { get; set; }

    public Game()
    {
        Party heroesParty = new Party(
            new List<Character>() { new TheTrueProgrammer() },
            PartyType.Heroes
        );
        Party monstersParty1 = new Party(
            new List<Character>() { new SkeletonCharacter() },
            PartyType.Monsters
        );
        Party monstersParty2 = new Party(
            new List<Character>() { new SkeletonCharacter(), new SkeletonCharacter() },
            PartyType.Monsters
        );
        Party uncodedOnesParty = new Party(
            new List<Character>() { new TheUncodedOne() },
            PartyType.Monsters
        );

        Battles = new List<Battle>()
        {
            new Battle(heroesParty, monstersParty1),
            new Battle(heroesParty, monstersParty2),
            new Battle(heroesParty, uncodedOnesParty)
        };
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
