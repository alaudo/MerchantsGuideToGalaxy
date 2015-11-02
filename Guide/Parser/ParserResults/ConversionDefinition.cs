using Guide.Universal;

namespace Guide.Parser
{
    /// <summary>
    /// Conversion definition ("xxx SMT is ### Credits")
    /// F# syntax: ConversionDefinition of RomanNumber * Roman * int
    /// </summary>
    public class ConversionDefinition : ParserResult
    {
        public readonly string Number;

        public readonly string Currency;

        public readonly int Credits;

        public ConversionDefinition(string number, string currency, int credits)
        {
            Number = number; Currency = currency; Credits = credits;
        }

    }
}