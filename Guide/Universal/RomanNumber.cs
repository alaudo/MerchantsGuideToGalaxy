using System.Collections.Generic;
using System.Linq;

namespace Guide.Universal
{
    /// <summary>
    ///     Represents a single Roman numeral
    /// </summary>
    public class RomanNumber : List<Roman>
    {
        /// <summary>
        ///     Primary constructor
        /// </summary>
        /// <param name="digits"></param>
        public RomanNumber(List<Roman> digits)
        {
            AddRange(digits);
        }

        /// <summary>
        ///     Whether the current number is valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return ValidateSingles(this) && Validate(this, Roman.M);
                // this should short-circuit if first validation fails
            }
        }

        #region converters

        public int ToInteger()
        {
            return RomanToInteger(this);
        }

        public override string ToString()
        {
            return string.Join(string.Empty, this.Select(d => d.ToString()));
        }

        #endregion

        #region static methods

        /// <summary>
        ///     Statis convertor to RomanNumber
        /// </summary>
        public static RomanNumber FromDigitsList(List<Roman> input)
        {
            return new RomanNumber(input);
        }


        /// <summary>
        ///     Checks if non-repetitive digits appear more than once in the number
        /// </summary>
        private static bool ValidateSingles(List<Roman> digits)
        {
            return digits.GroupBy(a => a).Where(g => g.Count() > 1).All(gr => gr.Key.IsRepeatable());
        }

        /// <summary>
        ///     Recursive static method to validate the given number
        /// </summary>
        /// <param name="digits">Number to validate</param>
        /// <param name="highest">Highest allowed digit</param>
        private static bool Validate(RomanNumber digits, Roman highest)
        {
            switch (digits.Count)
            {
                case 0:
                    return true; // the complete number is validated

                case 1:
                    return digits[0] <= highest; // next number to the right should always be equal or smaller

                default:
                    if (digits.Count >= 4 && digits.Take(4).Distinct().Count() == 1) // four in a row are not allowed
                    {
                        return false;
                    }

                    if (digits.Count >= 3 && digits.Take(3).Distinct().Count() == 1) // three in a row is OK
                    {
                        var current = digits[0].IsRepeatable(); // as long as they are Repeatable
                        return current && Validate(FromDigitsList(digits.Skip(3).ToList()), digits[1]);
                    }

                    if (digits[0] == digits[1]) // two in a row
                    {
                        var current = digits[0].IsRepeatable(); // should be repeatable too
                        return current && Validate(FromDigitsList(digits.Skip(2).ToList()), digits[0]);
                    }

                    if (digits[0] < digits[1]) // decreasing the main number
                    {
                        var current = digits[0].CanPrecede(digits[1]);

                        return current && Validate(FromDigitsList(digits.Skip(2).ToList()), digits[1]);
                    }
                    return Validate(FromDigitsList(digits.Skip(1).ToList()), digits[0]);
            }
        }

        /// <summary>
        ///     Converts Roman to integer
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        private static int RomanToInteger(RomanNumber digits)
        {
            switch (digits.Count)
            {
                case 0:
                    return 0;
                case 1:
                    return digits[0].ToInteger();
                default:
                    if (digits[0] < digits[1])
                    {
                        return (digits[1].ToInteger() - digits[0].ToInteger()) +
                               RomanToInteger(FromDigitsList(digits.Skip(2).ToList()));
                    }
                    return digits[0].ToInteger() + RomanToInteger(FromDigitsList(digits.Skip(1).ToList()));
            }
        }

        #endregion
    }
}