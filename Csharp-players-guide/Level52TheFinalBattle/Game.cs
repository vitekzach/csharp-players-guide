using System.Text.Json;
using Level52TheFinalBattle.Characters;
using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.Helpers;
using Level52TheFinalBattle.Items;

namespace Level52TheFinalBattle;

public class Game
{
    private List<Battle> Battles { get; set; }

    internal Game()
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

        Console.WriteLine("Before parser");
        var levelProvider = new GameJsonParser("Levels.json", heroActionChooser, monsterActionChooser);

        Battles = levelProvider.GetBattles(heroActionChooser, monsterActionChooser);
        Console.WriteLine(Battles.Count);
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

internal class GameJsonParser: ILevelProvider
{
    string _jsonPath;
    List<Dictionary<string, List<string>>>? _heroParty;
    List<Dictionary<string, List<string>>>? _monsterParties;
    Dictionary<string, List<Dictionary<string, List<string>>>>? _levels;
    ActionChooserEnum _heroActionChooser;
    ActionChooserEnum _monsterActionChooser;

    internal GameJsonParser(string jsonLevelsPath, ActionChooserEnum heroActionChooser, ActionChooserEnum monsterActionChooser )
    {
        _jsonPath = jsonLevelsPath;
        _heroActionChooser = heroActionChooser;
        _monsterActionChooser = monsterActionChooser;
    }

    public List<Battle> GetBattles(
    ActionChooserEnum heroActionChooser,
    ActionChooserEnum monsterActionChooser
        )
    {
      DeserializeJsonString(LoadJsonFile());
      GetParties();
      ValidatePartyCount();
      var battleList = new List<Battle>(){};
      foreach(Dictionary<string, List<string>> party in _heroParty!) 
        ValidateSingleParty(party);
      var heroParty = GetHeroPartyFromPartyDict(_heroParty[0]);
      var monsterParties = new List<Party>(){};
      foreach(Dictionary<string, List<string>> party in _monsterParties!) {
        ValidateSingleParty(party);
        battleList.Add(new Battle(heroParty, GetMonsterPartyFromPartyDict(party)));
      }

      return battleList;
    }

    private string LoadJsonFile()
    {
        if (!File.Exists(_jsonPath))
            throw new FileNotFoundException($"Level config file {_jsonPath} not found.");

        return File.ReadAllText(_jsonPath);
    }

    private void DeserializeJsonString(string jsonString) {
      _levels = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, List<string>>>>>(jsonString)!;
    }

    private void GetParties(){
      if (_levels!.TryGetValue("heroParty", out _heroParty) && _levels.TryGetValue("monsterParties", out _monsterParties)){}
      else{throw new FormatException("Your levels file has to contain heroParty and monsterParties keys.");}
    }
  
    // private List<Character> ParseParty<CharacterEnum>(){
    // }

    private void ValidatePartyCount(){
      if (_heroParty!.Count != 1)
          throw new FormatException("You have to specify exactly one hero party.");
      if (_monsterParties!.Count < 1)
          throw new FormatException("You have to specify at least one monster party.");
    }

    private void ValidateSingleParty(Dictionary<string, List<string>> party){
      if (party.TryGetValue("heroes", out List<string> heroes) && party.ContainsKey("inventory")){
        if (heroes.Count == 0)
          throw new FormatException("Each party needs to have at least one hero.");
      }
      else{
        throw new FormatException("Your parties need to contain keys heroes and inventory.");
      }
    }

    private Party GetHeroPartyFromPartyDict(Dictionary<string, List<string>> heroParty){
      var characterList = new List<Character>(){};
      foreach(string character in heroParty.GetValueOrDefault("heroes")!){
        if (!Enum.TryParse<HeroCharacter>(character,false,  out HeroCharacter characterParsed))
          throw new FormatException($"Unknown hero {characterParsed}.");
        characterList.Add(CharacterCreator.CreateHeroCharacter(characterParsed, _heroActionChooser));
      }

      var inventory = new List<InventoryItem>();
      foreach(string inventoryItem in heroParty.GetValueOrDefault("inventory")!){
        if (Enum.TryParse<HealingItemEnum>(inventoryItem, out HealingItemEnum healingItem)){
          inventory.Add(HealthPotionCreator.CreateHealthPotion(healingItem));
          continue;}
        if (Enum.TryParse<GearItemEnum>(inventoryItem, out GearItemEnum gearItem)){
          inventory.Add(GearCreator.CreateGearItem(gearItem));
          continue;}
        throw new FormatException($"Unkown inventory item {inventoryItem}.");
          }
      return new Party(characterList, PartyType.Heroes, inventory);
    }

    private Party GetMonsterPartyFromPartyDict(Dictionary<string, List<string>> monsterParty){
      var characterList = new List<Character>(){};
      foreach(string character in monsterParty.GetValueOrDefault("heroes")!){
        if (!Enum.TryParse<MonsterCharacter>(character,false,  out MonsterCharacter characterParsed))
          throw new FormatException($"Unknown hero {characterParsed}.");
        characterList.Add(CharacterCreator.CreateMonsterCharacter(characterParsed, _monsterActionChooser));
      }

      var inventory = new List<InventoryItem>();
      foreach(string inventoryItem in monsterParty.GetValueOrDefault("inventory")!){
        if (Enum.TryParse<HealingItemEnum>(inventoryItem, out HealingItemEnum healingItem)){
          inventory.Add(HealthPotionCreator.CreateHealthPotion(healingItem));
          continue;}
        if (Enum.TryParse<GearItemEnum>(inventoryItem, out GearItemEnum gearItem)){
          inventory.Add(GearCreator.CreateGearItem(gearItem));
          continue;}
        throw new FormatException($"Unkown inventory item {inventoryItem}.");
          }
      return new Party(characterList, PartyType.Monsters, inventory);
    }
}

internal interface ILevelProvider{
  internal List<Battle> GetBattles(ActionChooserEnum heroActionChooser,
        ActionChooserEnum monsterActionChooser
);
}

internal class DefaultLevelProvider:ILevelProvider{
  public List<Battle> GetBattles(
      ActionChooserEnum heroActionChooser,
        ActionChooserEnum monsterActionChooser
){
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
}
