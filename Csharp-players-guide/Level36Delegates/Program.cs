using Level36Delegates.Challenges;

Console.WriteLine("Which filger do you want?");
Console.WriteLine(" (0) Even");
Console.WriteLine(" (1) Positive");
Console.WriteLine(" (2) Tens");
Console.Write("Your choice: ");
string choice = Console.ReadLine() ?? "";
var sieve = (choice) switch
{
    "0" => new TheSieve(x => x % 2 == 0),
    "1" => new TheSieve(x => x > 0),
    "2" => new TheSieve(x => x % 10 == 0),
};
while (true)
{
    Console.Write("Number to be judged: ");
    Console.WriteLine(sieve.IsGood(Convert.ToInt32(Console.ReadLine())));
}