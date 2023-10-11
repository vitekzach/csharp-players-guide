using System.Numerics;
// public interface IMultiplyOperators<TSelf, TOther, TResult> where TSelf : IMultiplyOperators<TSelf, TOther, TResult>
// {
//   static abstract TResult operator *(TSelf a, TOther b);
// }

internal static class BlastDamageCalculator
{
  internal static T CalculateBlastDamage<T>(T initialDamage, T distance) where T : IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
  {
    return initialDamage / (distance * distance);
  }
}
