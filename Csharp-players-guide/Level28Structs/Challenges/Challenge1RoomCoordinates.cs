// •Create a Coordinate struct that can represent a room coordinate with a row and column.
// •Ensure Coordinate is immutable.
// •Make a method to determine if one coordinate is adjacent to another (differing only by a single row
//      or column).
// •Write a main method that creates a few coordinates and determines if they are adjacent to each
//      other to prove that it is working correctly.
// 
// 

namespace Level28Structs.Challenges;

public struct Coordinate
{
    public readonly int Row { get; }
    public readonly int Column { get; }

    public Coordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public static bool AreAdjacent(Coordinate a, Coordinate b)
    {
        if (Math.Abs(a.Row - b.Row) + Math.Abs(a.Column - b.Column) == 1) return true;
        return false;
    }
}
