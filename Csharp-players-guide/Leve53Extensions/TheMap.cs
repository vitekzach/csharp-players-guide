internal enum TerrainType
{
  Water,
  PlainLand,
  Mountain,
  City
};

internal class Map
{
  private readonly TerrainType[,] _map = new TerrainType[4, 8] { { TerrainType.Water, TerrainType.PlainLand, TerrainType.City, TerrainType.PlainLand, TerrainType.Water, TerrainType.PlainLand, TerrainType.Water, TerrainType.Water },
    {TerrainType.Mountain , TerrainType.PlainLand , TerrainType.PlainLand , TerrainType.PlainLand , TerrainType.Water , TerrainType.PlainLand , TerrainType.Water , TerrainType.Water},
    {TerrainType.Water , TerrainType.Mountain , TerrainType.PlainLand , TerrainType.PlainLand , TerrainType.PlainLand , TerrainType.PlainLand , TerrainType.PlainLand , TerrainType.PlainLand },
    {TerrainType.Water, TerrainType.Water , TerrainType.Mountain , TerrainType.City , TerrainType.Mountain , TerrainType.Water , TerrainType.Water , TerrainType.Water }
  };

  internal void PrintMap()
  {
    for (int i = 0; i < _map.GetLength(0); i++)
    {

      for (int j = 0; j < _map.GetLength(1); j++)
      {
        Console.Write(TerrainTypeToString(_map[i, j]));
      }
      Console.WriteLine();
    }
  }

  private static string TerrainTypeToString(TerrainType terrainType)
  {
    return terrainType switch
    {
      TerrainType.Water => "~~",
      TerrainType.Mountain => "^^",
      TerrainType.City => "()",
      TerrainType.PlainLand => "  ",
      _ => "xx"
    };
  }
}
