namespace Level39Files.Challenges;

public class TheLongGame
{
    private int UserScore { get; set; }
    private string UserName { get; init; }
    private string FileName { get; init; }

    public TheLongGame()
    {
        Console.Write("What's your username? ");
        UserName = Console.ReadLine() ?? "unknown";
        FileName = $"{UserName}.txt";
        GetUserScore();
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Score: {UserScore}");
            ConsoleKey pressedKey = Console.ReadKey().Key;
            UserScore++;
            if (pressedKey == ConsoleKey.Enter)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting...");
                Console.ResetColor();
                SaveUserScore();
                break;
            }
        }
    }

    private void GetUserScore()
    {
        if (File.Exists(FileName)) UserScore = Convert.ToInt32(File.ReadAllText(FileName));
    }
    
    private void SaveUserScore()
    {
        File.WriteAllText(FileName, Convert.ToString(UserScore));
    }
}