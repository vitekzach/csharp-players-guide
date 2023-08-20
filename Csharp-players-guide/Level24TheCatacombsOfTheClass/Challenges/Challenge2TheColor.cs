// •Define a new Color class with properties for its red, green, and blue channels.
// •Add appropriate constructors that you feel make sense for creating new Color objects.
// •Create static properties to define the eight commonly used colors for easy access.
// •In your main method, make two Color-typed variables. Use a constructor to create a color instance and use a
//          static property for the other. Display each of their red, green, and blue channel values.

namespace Level24TheCatacombsOfTheClass.Challenges;

public class Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }

    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }
    
    public static Color White = new Color(255, 255, 255);
    public static Color Black = new Color(0, 0, 0);
    public static Color Red = new Color(255, 0, 0);
    public static Color Orange = new Color(255,165, 0);
    public static Color Yellow = new Color(255, 255, 0);
    public static Color Green = new Color(0, 128, 0);
    public static Color Blue = new Color(0, 0, 255);
    public static Color Purple = new Color(128, 0, 128);
}