using System.Runtime.InteropServices.JavaScript;
using Level44AsynchronousProgramming.Challenges;

var gen = new RandomWordsGenerator();
// await gen.RecreateWord();
// gen.RecreateManyWords();

// var startTime = DateTime.UtcNow;
// await gen.RecreateCoupleWords();
// var endTime = DateTime.UtcNow;
// var timeRan = endTime - startTime;
// Console.WriteLine($"Runtime {timeRan.Hours} h, {timeRan.Minutes} m, {timeRan.Seconds} s.");


gen.CompareStringBuilding(attempts:1000000, lengths: 100);