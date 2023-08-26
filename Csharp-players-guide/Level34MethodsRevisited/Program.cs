using Level34MethodsRevisited.Challenges;

// int userInputInt = SafeNumberCruncher.CrunchIntNumber();
// Console.WriteLine($"You put in {userInputInt}.");
//
// double userInputDouble = SafeNumberCruncher.CrunchDoubleNumber();
// Console.WriteLine($"You put in {userInputDouble}.");
//
// bool userInputBool = SafeNumberCruncher.CrunchBool();
// Console.WriteLine($"You put in {userInputBool}.");

var random = new Random();
Console.WriteLine(random.NextDouble());
Console.WriteLine(random.NextDouble());
Console.WriteLine(random.NextDouble());
Console.WriteLine(random.NextDouble(20d));
Console.WriteLine(random.NextDouble(20d));
Console.WriteLine(random.NextDouble(20d));

Console.WriteLine(random.NextString("up", "down", "left", "right"));
Console.WriteLine(random.NextString("up", "down", "left", "right"));
Console.WriteLine(random.NextString("up", "down", "left", "right"));
Console.WriteLine(random.NextString("up", "down", "left", "right"));

Console.WriteLine(random.CoinFlip());
Console.WriteLine(random.CoinFlip());
Console.WriteLine(random.CoinFlip());
Console.WriteLine(random.CoinFlip(0.1d));
Console.WriteLine(random.CoinFlip(0.1d));
Console.WriteLine(random.CoinFlip(0.1d));
Console.WriteLine(random.CoinFlip(0.9d));
Console.WriteLine(random.CoinFlip(0.9d));
Console.WriteLine(random.CoinFlip(0.9d));

