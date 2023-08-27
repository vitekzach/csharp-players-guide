namespace Level43Threads.Challenges;

public class RecentNumbers
{
    private Random RandomGenerator { get;} = new Random();
    private readonly object _lockList = new object();
    private List<int> _recentNubmerList = new List<int>() { -2, -1 };


    public List<int> RecentNumbersList
    {
        get
        {
            lock (_lockList)
            {
                return _recentNubmerList;
            }
        }
    }

    public void Generate()
    {
        while (true)
        {
            int generatedNumber = RandomGenerator.Next(10);
            RecentNumbersList.RemoveAt(0);
            RecentNumbersList.Add(generatedNumber);
            Console.WriteLine($"Generated number: {generatedNumber}.");
            Thread.Sleep(1000);
        }
    }

    public void GuessSameNumbers()
    {
        while (true)
        {
            Console.ReadKey();
            if (RecentNumbersList[0] == RecentNumbersList[1]) Console.WriteLine("You're right!");
            else Console.WriteLine("You're NOT right!");
        }
    }
}