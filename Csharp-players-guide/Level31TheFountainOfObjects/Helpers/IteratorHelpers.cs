namespace Level31TheFountainOfObjects.Helpers;

public static class IteratorHelpers
{
    public static T[] EliminateNulls<T>(T[] iterable){
        T[] iterableWithoutNulls = iterable.Where(c => c != null).ToArray();
        return iterableWithoutNulls;
    }
}