using Guide.Universal;

namespace Guide.Parser
{
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
}