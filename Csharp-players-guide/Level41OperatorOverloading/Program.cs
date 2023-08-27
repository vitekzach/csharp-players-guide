using Level41OperatorOverloading.Challenges;

var baseCoordinate = new BlockCoordinate(4,3);
var offset = new BlockOffset(2, 0);

Console.WriteLine((baseCoordinate + offset).ToString());
Console.WriteLine((baseCoordinate + Direction.East).ToString());
Console.WriteLine(baseCoordinate[0]);
Console.WriteLine(baseCoordinate[1]);
Console.WriteLine(baseCoordinate[2]);
BlockOffset offsetFromDirection = Direction.North;
Console.WriteLine(offsetFromDirection.ToString());
