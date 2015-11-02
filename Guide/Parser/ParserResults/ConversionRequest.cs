using Guide.Universal;

namespace Guide.Parser
{
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
}