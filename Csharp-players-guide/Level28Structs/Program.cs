using Level28Structs.Challenges;

Coordinate baseCoordinate = new Coordinate(0, 0);

for (int row = -2; row <= 2; row++)
{
    for (int column = -2; column <= 2; column++)
    {
        Coordinate testCoord = new Coordinate(row, column);
        Console.WriteLine($"Base: ({baseCoordinate.Row}, {baseCoordinate.Column})");
        Console.WriteLine($"Test: ({testCoord.Row}, {testCoord.Column})");
        Console.WriteLine($"Adjacent: {Coordinate.AreAdjacent(baseCoordinate, testCoord)}");
        Console.WriteLine("________________________________________________________________");
    }
}