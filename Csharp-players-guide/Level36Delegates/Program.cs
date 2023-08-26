using Level36Delegates.Challenges;

Console.WriteLine("Which filger do you want?");
Console.WriteLine(" (0) Even");
Console.WriteLine(" (1) Positive");
Console.WriteLine(" (2) Tens");
Console.Write("Your choice: ");
string choice = Console.ReadLine() ?? "";
var sieve = (choice) switch
{
    "0" => new TheSieve(TheSieve.EvenFilter),
    "1" => new TheSieve(TheSieve.PositiveFilter),
    "2" => new TheSieve(TheSieve.TensFilter),
};
while (true)
{
    Console.Write("Number to be judged: ");
    Console.WriteLine(sieve.IsGood(Convert.ToInt32(Console.ReadLine())));
}