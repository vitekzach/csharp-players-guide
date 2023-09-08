using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle;

public class Game
{
   public Party HeroesParty { get; private set; }
   public Party MonstersParty { get; private set; }
   private List<Battle> Battles { get; set; }

   public Game()
   {
        HeroesParty = new Party(new List<Character>() { new TheTrueProgrammer() }, PartyType.Heroes);
        MonstersParty = new Party(new List<Character>() { new SkeletonCharacter() }, PartyType.Monsters);
        Battles = new List<Battle>();
        Battles.Add(new Battle(HeroesParty, MonstersParty));
   }

   public void Run()
   {
       foreach (Battle battle in Battles)
       {
           ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Battle, "New battle starts!");
           battle.RunBattle();
       }
   }
}