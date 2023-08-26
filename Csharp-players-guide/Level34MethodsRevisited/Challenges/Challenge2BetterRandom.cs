using System.Data.SqlTypes;

namespace Level34MethodsRevisited.Challenges;

public static class RandomExtensions
{
    public static double NextDouble(this Random random, double maxValue = 1) => random.NextDouble() * maxValue;

    public static string NextString(this Random random, params string[] args) => args[random.Next(args.Length)];

    public static bool CoinFlip(this Random random, double headsProbability = 0.5f) => random.NextDouble() < headsProbability;
}