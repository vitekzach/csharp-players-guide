internal static class ProgramMakingProgram
{
  internal static void MakeProgram()
  {
    Console.Write("Choose unit: ");
    string unit = Console.ReadLine();
    Console.Write("Choose type(int, float, double: ");
    string type = Console.ReadLine();

    string program = $$"""
      Console.WriteLine("Enter the width (in {{unit}}) of the triangle: ");
      {{type}} width = {{type}}.Parse(Console.ReadLine());
      Console.WriteLine("Enter the height (in {{unit}}) of the triangle: ");
      {{type}} height = {{type}}.Parse(Console.ReadLine());
      {{type}} result = width * height / 2;
      Console.WriteLine($"{result} square {{unit}}");
      """;
    Console.WriteLine(program);
  }
}
