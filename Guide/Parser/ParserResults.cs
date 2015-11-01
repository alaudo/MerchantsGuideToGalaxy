using Guide.Universal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Parser
{
    /// <summary>
    /// Discriminated union pattern in OOP, see F# for explanation
    /// </summary>
    public abstract class ParserResult
    {

    }

    /// <summary>
    /// Digit definition class ("xxx is L")
    /// F# syntax: DigitDefinition of string * Roman
    /// </summary>
    public class DigitDefinition: ParserResult
    {
        public readonly string Word;

        public readonly Roman Digit;

        public DigitDefinition(string word, Roman digit)
        {
            Word = word; Digit = digit;
        }
    }

    /// <summary>
    /// Conversion definition ("xxx SMT is ### Credits")
    /// F# syntax: ConversionDefinition of RomanNumber * Roman * int
    /// </summary>
    public class ConversionDefinition : ParserResult
    {
        public readonly RomanNumber Number;

        public readonly string Currency;

        public readonly int Credits;

        public ConversionDefinition(RomanNumber number, string currency, int credits)
        {
            Number = number; Currency = currency; Credits = credits;
        }

    }

    /// <summary>
    /// Request to convert value ("how much/many xxx SMT ?")
    /// F# syntax: ConversionRequest of RomanNumber * string * string
    /// </summary>
    public class ConversionRequest : ParserResult
    {
        public readonly RomanNumber Number;

        public readonly string Currency;

        public readonly string Original;

        public ConversionRequest(RomanNumber number, string currency, string original)
        {
            Number = number; Currency = currency; Original = original;
        }

    }

    /// <summary>
    /// Failed to parse result or parsed data failed validation
    /// F# syntax: InvalidResult of string
    /// </summary>
    public class InvalidResult : ParserResult
    {
        public readonly string Original;

        public InvalidResult(string original)
        {
            Original = original;
        }

    }

}
