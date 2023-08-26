namespace Level36Delegates.Challenges;

public class TheSieve
{
    private Predicate<int> Predicate { get; init; }
    
    public TheSieve(Predicate<int> predicate)
    {
        Predicate = predicate;
    }

    public bool IsGood(int number) => Predicate.Invoke(number);
    
    public static bool EvenFilter(int inputNumber) => inputNumber % 2 == 0;
    public static bool PositiveFilter(int inputNumber) => inputNumber > 0;
    public static bool TensFilter(int inputNumber) => inputNumber % 10 == 0;
}