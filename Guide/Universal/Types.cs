using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Universal
{
   /// <summary>
   /// Enumeration with intergrated mapping
   /// </summary>
   public enum Roman 
    {
        I = 1,
        V = 5,
        X = 10,
        L = 50,
        C = 100,
        D = 500,
        M = 1000
    }

    /// <summary>
    /// Container for Roman enum extensions (to allow fluent interface syntax)
    /// </summary>
    public static class RomanExtensions
    {
        /// <summary>
        /// If the string is the valid Roman digit
        /// </summary>
        /// <param name="test">string to test</param>
        /// <returns></returns>
        public static bool IsRoman(this string test)
        {
            return Enum.IsDefined(typeof(Roman), test);
        }

        /// <summary>
        /// Converts string to Roman if possible
        /// </summary>
        public static Roman ToRoman(this string test)
        {
            return (Roman)Enum.Parse(typeof(Roman), test);
        }

        /// <summary>
        /// Converts Roman digit to its integer value
        /// </summary>
        public static int ToInteger(this Roman digit)
        {
            return (int)digit;
        }

        /// <summary>
        /// If the Roman digit can be repeated 
        /// </summary>
        public static bool IsRepeatable(this Roman digit)
        {
            return     (digit == Roman.I)
                    || (digit == Roman.X)
                    || (digit == Roman.C)
                    || (digit == Roman.M);

        }

        /// <summary>
        /// If this Roman digit can appear only once
        /// </summary>
        public static bool IsSingle(this Roman digit)
        {
            return (digit == Roman.V)
                    || (digit == Roman.L)
                    || (digit == Roman.D);
        }

    }

    /// <summary>
    /// Exchange rates
    /// </summary>
    public struct ExchangeRate
    {
        public readonly string Currency;
        public readonly int Amount;
        public readonly int Value;
    }

    /// <summary>
    /// Current exchange rates on Martian market
    /// </summary>
    public class ExchangeRates : Dictionary<string, ExchangeRate>
    {
        // TODO: re-write it
    }

    /// <summary>
    /// Dictionary of the Martian number language
    /// </summary>
    public class Martian : Dictionary<string, Roman>
    {
        // TODO: re-write it
    }

    public class RomanNumber : List<Roman>
    {
        public bool IsValid {
            get
            {
                return ValidateSingles(this) && Validate(this, Roman.M);
            }

            private set { } }

        public override string ToString()
        {
            return string.Join(string.Empty, this.Select(d => d.ToString()));
        }

        public int ToInteger()
        {
            return RomanToInteger(this);
        }


        private static bool ValidateSingles(List<Roman> digits)
        {
            return digits.GroupBy(a => a).Where(g => g.Count() > 1).All(gr => !gr.Key.IsSingle());
        }

        private static bool Validate(List<Roman> digits, Roman highest)
        {
            switch (digits.Count)
            {
                case 0: return true;

                case 1: return digits[0] <= highest; // next number should always be smaller

                default:
                        if (digits.Count >= 4 && digits.Take(4).Distinct().Count() == 1) // four in a row
                        {
                            return false;
                        }

                        if (digits.Count >= 3 && digits.Take(3).Distinct().Count() == 1)
                        {
                            bool current = digits[0].IsRepeatable();
                            return current && Validate(digits.Skip(3).ToList(), digits[1]);
                        }

                        if (digits[0] == digits[1])
                        {
                            bool current = digits[0].IsRepeatable();
                            return current && Validate(digits.Skip(2).ToList(), digits[0]);
                        }

                    if (digits[0] < digits[1])
                        {
                            bool current = (digits[0] == Roman.I && digits[1] == Roman.V)
                                        || (digits[0] == Roman.I && digits[1] == Roman.X)
                                        || (digits[0] == Roman.X && digits[1] == Roman.L)
                                        || (digits[0] == Roman.X && digits[1] == Roman.C)
                                        || (digits[0] == Roman.C && digits[1] == Roman.D)
                                        || (digits[0] == Roman.C && digits[1] == Roman.M);

                            return current && Validate(digits.Skip(2).ToList(), digits[1]);
                        }
                        else
                        {
                            return Validate(digits.Skip(1).ToList(), digits[0]);
                        }
            }
        }

        private static int RomanToInteger(List<Roman> digits)
        {
            switch(digits.Count)
            {
                case 0: return 0;
                case 1: return digits[0].ToInteger();
                default: if (digits[0] < digits[1])
                    {
                        return (digits[1].ToInteger() - digits[0].ToInteger()) + RomanToInteger(digits.Skip(2).ToList());
                    }
                    else
                    {
                        return digits[0].ToInteger() + RomanToInteger(digits.Skip(1).ToList());
                    }
            }
        }

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="digits"></param>
        public RomanNumber(List<Roman> digits)
        {
            AddRange(digits);
        }

    }
}
