using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;

namespace Level52TheFinalBattle;

public class Battle
{
    public Party HeroesParty { get; private set; }
    public Party MonstersParty { get; private set; }

    public Battle()
    {
        HeroesParty = new Party(new List<Character>() { new TheTrueProgrammer() }, PartyType.Heroes);
        MonstersParty = new Party(new List<Character>() { new SkeletonCharacter() }, PartyType.Monsters);
    }

    public Battle(Party heroes, Party monsters)
    {
        HeroesParty = heroes;
        MonstersParty = monsters;
    }

    public void RunBattle()
    {
        while (true)
        {
            RunRound();
        }
    }


    private void RunRound()
    {
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Time, DateTime.UtcNow.ToString());
        TakeTurnForParty(HeroesParty);
        TakeTurnForParty(MonstersParty);
        Thread.Sleep(500);
    }

    private void TakeTurnForParty(Party party)
    {
        ConsoleHelpers.WriteLineWithColoredConsole(MessageType.Team,$"TEAM TURN: It is {party.Type}' turn.");
        foreach (Character character in party.Members)
        {
            Console.WriteLine($"It is {character.Name}'s turn...");
            character.TakeTurn(this);
            Console.WriteLine();
        }
    }

    public Party GetPartyFor(Character character) {
        if (HeroesParty.Members.Contains(character)) return HeroesParty;
        else return  MonstersParty;
    }

    public Party GetEnemyPartyFor(Character character) {
        if (!HeroesParty.Members.Contains(character)) return HeroesParty;
        else return  MonstersParty;
    }
}