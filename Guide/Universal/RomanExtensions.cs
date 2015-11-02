using System;

namespace Guide.Universal
{
    /// <summary>
    ///     Container for Roman enum extensions (to allow fluent interface syntax)
    /// </summary>
    public static class RomanExtensions
    {
        /// <summary>
        ///     If the string is the valid Roman digit
        /// </summary>
        /// <param name="test">string to test</param>
        /// <returns></returns>
        public static bool IsRoman(this string test)
        {
            return Enum.IsDefined(typeof (Roman), test);
        }

        /// <summary>
        ///     Converts string to Roman if possible
        /// </summary>
        public static Roman ToRoman(this string test)
        {
            return (Roman) Enum.Parse(typeof (Roman), test);
        }

        /// <summary>
        ///     Converts Roman digit to its integer value
        /// </summary>
        public static int ToInteger(this Roman digit)
        {
            return (int) digit;
        }

        /// <summary>
        ///     If the Roman digit can be repeated
        /// </summary>
        public static bool IsRepeatable(this Roman digit)
        {
            return (digit == Roman.I)
                   || (digit == Roman.X)
                   || (digit == Roman.C)
                   || (digit == Roman.M);
        }

        public static bool CanPrecede(this Roman left, Roman right)
        {
            return
                (left == Roman.I && right == Roman.V)
                || (left == Roman.I && right == Roman.X)
                || (left == Roman.X && right == Roman.L)
                || (left == Roman.X && right == Roman.C)
                || (left == Roman.C && right == Roman.D)
                || (left == Roman.C && right == Roman.M);
        }
    }
}