namespace Level44AsynchronousProgramming.Challenges;



public class RandomWordsGenerator
{
   private ulong RandomlyRecreate(string word)
   {
      int wordLength = word.Length;
      string generatedWord;
      var random = new Random();
      ulong attempts = 0;

      var newWord = new System.Text.StringBuilder();
      do
      {
         attempts++;
         newWord.Clear();
         for (int i = 0; i < wordLength; i++)
         {
            newWord.Append((char)('a' + random.Next(26)));
         }

         generatedWord = newWord.ToString();

      } while (generatedWord != word.ToLower());

      return attempts;

   }

   private Task<ulong> RandomlyRecreateAsync(string word)
   {
      return Task.Run(() =>  RandomlyRecreate(word));
   }

   private async Task RandomlyRecreateAsyncWithReport(string word)
   {
      var startTime = DateTime.UtcNow;
      ulong attempts = await RandomlyRecreateAsync(word);
      var endTime = DateTime.UtcNow;
      var timeRan = endTime - startTime;
      Console.WriteLine($"{word.ToUpper()}: {attempts:N0} attempts. Runtime {timeRan.Hours} h, {timeRan.Minutes} m, {timeRan.Seconds} s.");
   }

   public async Task RecreateWord()
   {
      Console.Write("What word would you like recreated? ");
      string word = Console.ReadLine() ?? "";
      var startTime = DateTime.UtcNow;
      ulong attempts = await RandomlyRecreateAsync(word);
      var endTime = DateTime.UtcNow;
      var timeRan = endTime - startTime;
      Console.WriteLine($"It took {attempts:N0} attempts to guess the word \"{word}\".");
      Console.WriteLine($"The time to guess was {timeRan.Hours} hours, {timeRan.Minutes} minutes, {timeRan.Seconds} seconds.");
   }

   public async Task RecreateManyWords()
   {
      Console.WriteLine("Keep inputting words and I will print results as I go.");
      string word;

      while (true)
      {
         word = Console.ReadLine() ?? "x";
         RandomlyRecreateAsyncWithReport(word);
      }
   }
}