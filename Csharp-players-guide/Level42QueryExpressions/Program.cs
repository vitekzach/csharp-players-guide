using Level42QueryExpressions.Challenges;


var input = new int[] {1, 9, 2, 8, 3, 7, 4, 6, 5 };
foreach (List<int> listOfInt in Arraynator.DoThreeLensesWithLoops(input))
{
    Console.WriteLine(String.Join(", ", listOfInt));
}
Console.WriteLine("----------------------------------------------");
foreach (List<int> listOfInt in Arraynator.DoThreeLensesWithKeywordBased(input))
{
    Console.WriteLine(String.Join(", ", listOfInt));
}
Console.WriteLine("----------------------------------------------");
foreach (List<int> listOfInt in Arraynator.DoThreeLensesWithMethodBased(input))
{
    Console.WriteLine(String.Join(", ", listOfInt));
}