using Level45DynamicObjects.Challenges;

Console.WriteLine(AddUniter.Add((int)1, (int)2));
Console.WriteLine(AddUniter.Add((double)1, (double)2));
Console.WriteLine(AddUniter.Add("Hello", " World"));
Console.WriteLine(AddUniter.Add(DateTime.UtcNow, TimeSpan.FromDays(1)));


TheRobotFactory.StartRobotFactory();