using Guide.Universal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Parser
{
    /// <summary>
    /// Main abstract class of the union
    /// </summary>
    public abstract class ParserResult
    {

    }

    /// <summary>
    /// Digit definition class
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

    public class InvalidResult : ParserResult
    {
        public readonly string Original;

        public InvalidResult(string original)
        {
            Original = original;
        }

    }

}
