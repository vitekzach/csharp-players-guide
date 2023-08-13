// Challenge name: The Variable Shop
// Objectives:
//  •Build a program with a variable of all fourteen types described in this level.
//  •Assign each of them a value using a literal of the correct type.
//  •Use Console.WriteLine to display the contents of each variable.

namespace _06_type_system.Challenges
{
    public class Challenge1
    {
        public static void Run()
        {
            // Integers
            byte intByte = 255;
            Console.WriteLine($"byte: {intByte}");
            short intShort = -32_768;
            Console.WriteLine($"short: {intShort}");
            int intInt = -2_147_483_648;
            Console.WriteLine($"int: {intInt}");
            long intLong = -9_223_372_036_854_775_808;
            Console.WriteLine($"long: {intLong}");
            sbyte intSByte = -128;
            Console.WriteLine($"sbyte: {intSByte}");
            ushort intUShort = 65_535;
            Console.WriteLine($"ushort: {intUShort}");
            uint intUInt = 4_294_967_295U;
            Console.WriteLine($"uint: {intUInt}");
            ulong intULong = 18_446_744_073_709_551_615UL;
            Console.WriteLine($"ulong: {intULong}");
            
            // Characters and strings
            char charChar = 'a';
            Console.WriteLine($"char: {charChar}");
            string charString = "General Kenobi.";
            Console.WriteLine($"string: {charString}");
            
            // Floating point
            float floatFloat = 5e35f;
            Console.WriteLine($"float: {floatFloat}");
            double floatDouble = 5e300d;
            Console.WriteLine($"double: {floatDouble}");
            decimal floatDecimal = 7e27m;
            Console.WriteLine($"decimal: {floatDecimal}");
            
            // Boolean
            bool boolBool = true;
            Console.WriteLine($"bool: {boolBool}");
        }
    }
}