using Humanizer;

var feastTime = DateTime.UtcNow.AddHours(2.5);

// old way
Console.WriteLine($"When is the feast? {feastTime}");

// new way
Console.WriteLine($"When is the feast? {feastTime.Humanize()}");
