using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("42*47=1?74", 9);
            Test("4?*47=1974", 2);
            Test("42*?7=1974", 4);
            Test("42*?47=1974", -1);
            Test("2*12?=247", -1);
            Console.ReadKey(true);
        }

        private static void Test(string args, int expected)
        {
            var result = FindDigit(args).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{args} : {result}");
        }

        public static int FindDigit(string equation)
        {
            // Add your code here.
            int equalsIndex = equation.IndexOf('=');
            int asteriskIndex = equation.IndexOf('*');
            int questionMarkIndex = equation.IndexOf('?');

            string number1 = equation.Substring(0, asteriskIndex);
            string number2 = equation.Substring(asteriskIndex + 1, equalsIndex - asteriskIndex - 1);
            string product = equation.Substring(equalsIndex + 1);

            // make appropriate function call to get required number
            if (product.IndexOf('?') > 0)
            {
                return GetRequiredNumber(number1, number2, product, true);
            }
            else if (number1.IndexOf('?') > 0)
            {
                return GetRequiredNumber(number2, product, number1);
            }
            else
            {
                return GetRequiredNumber(number1, product, number2);
            }
        }

        private static int GetRequiredNumber(string argument1, string argument2, string argument3, bool product = false)
        {
            int result, temp = argument3.Length, remainder = 0;

            // compute appropriate "result" as per the "product" argument
            if (product)
            {
                result = Int32.Parse(argument1) * Int32.Parse(argument2);
            }
            else
            {
                result = Int32.Parse(argument2) / Int32.Parse(argument1);
                // check divisibility
                if (result * Int32.Parse(argument1) != Int32.Parse(argument2))
                {
                    return -1;
                }
            }

            // check for leading zeros
            if ((result / (int)Math.Pow(10.0, (double)(temp - 1))) < 1)
            {
                return -1;
            }

            // compute the required number literal
            for (int index = 0; index < temp; ++index)
            {
                remainder = result % (int)Math.Pow(10.0, 1.0);
                result /= (int)Math.Pow(10.0, 1.0);
                if (argument3[temp - 1 - index] == '?')
                {
                    result = remainder;
                    break;
                }
            }
            return result;
        }
    }
}
