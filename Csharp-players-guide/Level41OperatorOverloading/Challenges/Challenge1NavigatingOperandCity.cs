namespace Level41OperatorOverloading.Challenges;

public record BlockCoordinate(int Row, int Column)
{
    public static BlockCoordinate operator +(BlockCoordinate coordinate, BlockOffset offset) =>
        new BlockCoordinate(coordinate.Row + offset.RowOffset, coordinate.Column + offset.ColumnOffset);

    public static BlockCoordinate operator +(BlockCoordinate coordinate, Direction direction)
    {
        return coordinate + direction switch
        {
            Direction.North => new BlockOffset(-1, 0),
            Direction.South => new BlockOffset(1, 0),
            Direction.East => new BlockOffset(0, 1),
            Direction.West => new BlockOffset(0, -1),
        };
    }

    public int this[int index]
    {
        get
        {
            return index switch
            {
                0 => Row,
                1 => Column,
            };
        }
    } 
}




public record BlockOffset(int RowOffset, int ColumnOffset)
{
    public static implicit operator BlockOffset(Direction direction)
    {
        return direction switch
        {
            Direction.North => new BlockOffset(-1, 0),
            Direction.South => new BlockOffset(1, 0),
            Direction.East => new BlockOffset(0, 1),
            Direction.West => new BlockOffset(0, -1),
        };
    }
}


public enum Direction { North, East, South, West }