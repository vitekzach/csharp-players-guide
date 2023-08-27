using Level43Threads.Challenges;

var recentNumbers = new RecentNumbers();

Thread threadGenerator = new Thread(recentNumbers.Generate);
Thread inputReader = new Thread(recentNumbers.GuessSameNumbers);

threadGenerator.Start();
inputReader.Start();

