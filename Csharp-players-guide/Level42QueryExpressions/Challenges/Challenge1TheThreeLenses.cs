using System.Runtime.InteropServices.JavaScript;

namespace Level42QueryExpressions.Challenges;

public static class Arraynator
{
    public static List<List<int>> DoThreeLensesWithLoops(int[] array)
    {
        var outerToReturn = new List<List<int>>();
        
        // evens stevens
        var onlyEven = new List<int>();
        foreach (int member in array)
        {
            if (member % 2 == 0) onlyEven.Add(member);
        }
        outerToReturn.Add(onlyEven);

        // sort it
        var sorted = onlyEven.ToList();
        sorted.Sort();
        outerToReturn.Add(sorted);
        
        // double it 
        outerToReturn.Add(new List<int>());
        foreach (int member in sorted) outerToReturn[^1].Add(member * 2);

        return outerToReturn;
    }

    public static List<List<int>> DoThreeLensesWithKeywordBased(int[] array)
    {
        var outerToReturn = new List<List<int>>();
        
        //evens
        IEnumerable<int> evens = from n in array
                                 where n % 2 == 0
                                 select n;
        outerToReturn.Add(evens.ToList());

        //sort it
        IEnumerable<int> sorted = from n in evens
                                  orderby n
                                  select n;
        outerToReturn.Add(sorted.ToList());
        
        //double it
        IEnumerable<int> doubled = from n in sorted
                                   select n * 2;
        outerToReturn.Add(doubled.ToList());

        return outerToReturn;
    }
    
    public static List<List<int>> DoThreeLensesWithMethodBased(int[] array)
    {
        var outerToReturn = new List<List<int>>();
        
        //evens
        IEnumerable<int> evens = array.Where(e => e % 2 == 0);
        outerToReturn.Add(evens.ToList());

        //sort it
        IEnumerable<int> sorted = evens.Order();
        outerToReturn.Add(sorted.ToList());
        
        //double it
        IEnumerable<int> doubled = sorted.Select(s => s * 2);
        outerToReturn.Add(doubled.ToList());

        return outerToReturn;
    }
} 