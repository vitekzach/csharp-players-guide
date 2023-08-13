// Challenge name:
// Objectives:
//      •Modify your Variable Shop program to assign a new, different literal value to each of the 14 original
//          variables. Do not declare any additional variables.
//      •Use Console.WriteLine to display the updated contents of each variable.

namespace _06_type_system.Challenges
{
    public class Challenge2
    {
        public static void Run()
        {
            // Integers
            byte intByte = 255;
            intByte = 0;
            Console.WriteLine($"byte: {intByte}");
            short intShort = -32_768;
            intShort = 32_767;
            Console.WriteLine($"short: {intShort}");
            int intInt = -2_147_483_648;
            intInt = 2_147_483_647;
            Console.WriteLine($"int: {intInt}");
            long intLong = -9_223_372_036_854_775_808;
            intLong = 9_223_372_036_854_775_807;
            Console.WriteLine($"long: {intLong}");
            sbyte intSByte = -128;
            intSByte = 127;
            Console.WriteLine($"sbyte: {intSByte}");
            ushort intUShort = 65_535;
            intUShort = 0;
            Console.WriteLine($"ushort: {intUShort}");
            uint intUInt = 4_294_967_295U;
            intUInt = 0U;
            Console.WriteLine($"uint: {intUInt}");
            ulong intULong = 18_446_744_073_709_551_615UL;
            intULong = 0UL;
            Console.WriteLine($"ulong: {intULong}");
            
            // Characters and strings
            char charChar = 'a';
            charChar = 'z';
            Console.WriteLine($"char: {charChar}");
            string charString = "General Kenobi.";
            charString = "POWEEEEEEEEEER.";
            Console.WriteLine($"string: {charString}");
            
            // Floating point
            float floatFloat = 5e35f;
            floatFloat = -5e35f;
            Console.WriteLine($"float: {floatFloat}");
            double floatDouble = 5e300d;
            floatDouble = -5e300d;
            Console.WriteLine($"double: {floatDouble}");
            decimal floatDecimal = 7e27m;
            floatDecimal = -7e27m;
            Console.WriteLine($"decimal: {floatDecimal}");
            
            // Boolean
            bool boolBool = true;
            boolBool = false;
            Console.WriteLine($"bool: {boolBool}");
        }
    }
}