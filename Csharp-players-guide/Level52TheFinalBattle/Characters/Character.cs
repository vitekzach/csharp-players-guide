using Level52TheFinalBattle.Enums;
using Level52TheFinalBattle.ActionChoosers;

namespace Level52TheFinalBattle.Characters;

public class Character
{
    public string Name { get; init; }
    public List<CharacterMoves> Moves { get; init; }
    private IChooseActionInterface ActionChooser { get; init; }

    public Character(string name, IChooseActionInterface actionChooser)
    {
        Name = name;
        Moves = new List<CharacterMoves>() { CharacterMoves.Nothing };
        ActionChooser = actionChooser;
    }

    public void TakeTurn()
    {
        CharacterMoves move = ActionChooser.ChooseAction(this);
        Console.WriteLine($"{Name} did {move}.");
    }
}