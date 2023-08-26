namespace Level35ErrorHandlingAndExceptions.Challenges;

public class BadCookieEaten : Exception
{
    public BadCookieEaten(){}
    public BadCookieEaten(string message): base(message) {}
    public BadCookieEaten(string message, Exception otherException): base(message, otherException) {}
}

public class ExceptisGame
{
    private int RaisinPosition{get; init; }
    private List<int> PickedNumbers { get; set; }

    public ExceptisGame()
    {
        RaisinPosition = new Random().Next(10);
        PickedNumbers = new List<int>();
        
        string playerName;
        bool player1Turn = true;

        try
        {
            while (true)
            {
                playerName = (player1Turn) ? "Player1" : "Player2";
                Console.Write($"{playerName}'s turn. Pick a number: ");
                string playerInput = Console.ReadLine() ?? "";
                if (!int.TryParse(playerInput, out int playerInputInt)) continue;
                if (playerInputInt < 0 || playerInputInt > 9) continue;
                if (playerInputInt == RaisinPosition) throw new BadCookieEaten($"{playerName} ate the bad cookie!");
                if (PickedNumbers.Contains(playerInputInt)) continue;
                PickedNumbers.Add(playerInputInt);


                player1Turn = !player1Turn;
            }
        }
        catch (BadCookieEaten e)
        {
            Console.WriteLine($"Someone ate the bad cookie. Message from game: {e.Message}");
        }
        finally
        {
            Console.WriteLine("Game is over now.");
        }
        
        


    }
}