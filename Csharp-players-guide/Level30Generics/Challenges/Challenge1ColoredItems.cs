// •Put the three class definitions above into a new project.
// •Define a generic class to represent a colored item. It must have properties for the item itself (generic
//      in type) and a ConsoleColor associated with it.
// •Add a void Display() method to your colored item type that changes the console’s foreground
//      color to the item’s color and displays the item in that color. (Hint: It is sufficient to just call
//      ToString() on the item to get a text representation.)
// •In your main method, create a new colored item containing a blue sword, a red bow, and a green axe.
//      Display all three items to see each item displayed in its color.

using System.Drawing;

namespace Level30Generics.Challenges;

public class Sword { }
public class Bow { }
public class Axe { }

public class ColoredItem<T>
{
    public ConsoleColor ConsoleColor { get; }
    public T Item { get; }

    public ColoredItem(ConsoleColor color, T item)
    {
        ConsoleColor = color;
        Item = item;
    }

    public void Display()
    {
        Console.ForegroundColor = ConsoleColor;
        Console.WriteLine(Item.ToString());
        Console.ResetColor();
    }
}
